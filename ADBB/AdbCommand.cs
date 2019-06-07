using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ADBB
{
    public class AdbCommand
    {
        public class AdbProgressData
        {
            public string Message { get; }
            public bool IsError { get; }
            public bool IsSuccess { get; }
            public Exception Ex { get; }

            public AdbProgressData(string message, bool isError = false,bool isSuccess = false)
            {
                Message = message;
                IsError = isError;
                IsSuccess = isSuccess;
            }
            public AdbProgressData(string message, Exception ex)
            {
                Message = message + ":" + ex.Message;
                Ex = ex;
                IsError = true;
            }
        }

        public string AdbCommandPath { get; }

        public AdbCommand(string adbCommandPath)
        {
            AdbCommandPath = adbCommandPath;
        }

        private Task<string[]> Cmd(Device device, params string[] args)
        {
            return Cmd(CancellationToken.None, device, args);
        }

        private Task<string[]> Cmd(CancellationToken ct, Device device, params string[] args)
        {
            return Task.Run(() => {
                var tcs = new TaskCompletionSource<string[]>();
                var pro = new Process();

                if (ct != CancellationToken.None)
                {
                    ct.Register(() => {
                        tcs.TrySetCanceled(ct);
                        try
                        {
                            pro.Kill();
                        }
                        catch
                        {

                        }
                    });
                }

                var app = pro.StartInfo;
                app.FileName = AdbCommandPath ?? "adb";

                app.Arguments = string.Join(" ", args);

                if (device != null)
                {
                    app.Arguments = $"-s {device.Name} " + app.Arguments;
                }

                app.CreateNoWindow = true;
                app.UseShellExecute = false;
                app.RedirectStandardOutput = true;

                pro.Start();

                var output = pro.StandardOutput.ReadToEnd();

                tcs.TrySetResult(output.Replace("\r", "").Split(new[]{
                    "\n"
                }, StringSplitOptions.RemoveEmptyEntries));

                return tcs.Task;
            },ct);
        }

        public Task<IEnumerable<Device>> GetDeviceList(IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("デバイス一覧取得", progress, async () => {
                var result = await Cmd(null, "devices");
                return result.Skip(1).Select(s => s.Split(new[] { "\t" }, StringSplitOptions.None)).Select(s => new Device(s[0], s[1]));
            });
        }

        public Task<IEnumerable<PackageData>> GetPackageList(Device device, IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("パッケージリスト取得", progress, async () => {
                var result = await Cmd(device, "shell pm list package", "-3");
                return result.Skip(0).Select(s => s.Split(new[] { ":" }, StringSplitOptions.None)).Select(s => new PackageData(s[1]));
            });
        }

        public Task<bool> UnInstallPackage(Device device, PackageData package, IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("アンインストール", progress, async () => {
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
                {
                    var result = await Cmd(cts.Token, device, "uninstall", package.Name);
                    var isSuccess = result.FirstOrDefault()?.IndexOf("Success") >= 0;
                    if (isSuccess) return true;
                    throw new Exception();
                }
            });
        }

        public Task<bool> LunchPackage(Device device, PackageData package, IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("アプリケーション起動", progress, async () => {
                var dumpResult = await Cmd(device, "shell", $"\"pm dump {package.Name} | grep -A 2 android.intent.action.MAIN | head -2 | tail -1\"");
                var packageActivityName = dumpResult[1].Split(new[]{" "}, StringSplitOptions.RemoveEmptyEntries).ElementAtOrDefault(1);
                var result = await Cmd(device, "shell am start", "-n", packageActivityName);

                var isSuccess = result.FirstOrDefault()?.IndexOf("Success") >= 0 || result.FirstOrDefault()?.IndexOf("Starting") >= 0;
                if (isSuccess) return true;

                throw new Exception();
            });
        }

        public Task<bool> StopPackage(Device device, PackageData package, IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("アプリケーション停止処理", progress, async() => {
                var result = await Cmd(device, "shell am force-stop", package.Name);
                var isSuccess = result.FirstOrDefault()?.IndexOf("Success") >= 0 || result.FirstOrDefault()?.IndexOf("Starting") >= 0;
                if (isSuccess) return true;
                throw new Exception();
            });
        }

        public Task<bool> InstallPackage(Device device, string filePath, IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("インストール", progress, async () => {
                var result = await Cmd(device, "install", "-r", filePath);

                var isSuccess = result.Any(s => s.IndexOf("Success") >= 0);
                if (isSuccess) return true;
                throw new Exception();
            });
        }

        public Task<bool> ConnectIp(Device device,IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("IP接続", progress, async () => {
                var result = await Cmd(device, "shell", "\"ifconfig wlan0 | grep 'inet addr:' | sed -e 's/^.*inet addr://' -e 's/ .*//'\"");
                await Cmd(device, "tcpip", "5555");
                var connectResult = await Cmd(device, "connect", $"{result[0]}:5555");
                if (connectResult.Any(s => s.IndexOf("connected to") >= 0)) return true;
                throw new Exception();
            });
        }

        public Task<bool> DisconnectIp(IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("IP切断", progress, async () => {
                await Cmd(null, "disconnect");
                return true;
            });
        }

        public Task<bool> Shutdown(IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("シャットダウン", progress, async () => {
                await Cmd(null, "shell", "reboot -p");
                return true;
            });
        }

        private async Task<T> ProgressWrap<T>(string name,IProgress<AdbProgressData>progress, Func<Task<T>> proc)
        {
            try
            {
                progress.Report(new AdbProgressData(name + "開始"));
                var result  = await proc.Invoke();
                progress.Report(new AdbProgressData(name + "成功",false,true));
                return result;
            }
            catch (Exception ex)
            {
                progress.Report(new AdbProgressData(name + "失敗",ex));
            }
            return default(T);
        }
    }
}
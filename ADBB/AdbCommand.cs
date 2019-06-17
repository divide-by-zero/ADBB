using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ADBB
{
    /// <summary>
    /// AdbCommand実行用クラス
    /// </summary>
    public class AdbCommand
    {
        /// <summary>
        /// 非同期実行中進捗コールバック用データ
        /// </summary>
        public class AdbProgressData
        {
            public string Message { get; }
            public bool IsError { get; }
            public bool IsSuccess { get; }
            public bool IsRequireDialog { get; }
            public Exception Ex { get; }

            public AdbProgressData(string message, bool isError = false,bool isSuccess = false,bool? isRequireDialog = null)
            {
                Message = message;
                IsError = isError;
                IsSuccess = isSuccess;
                IsRequireDialog = isRequireDialog ?? isError;   //エラーの場合は大抵ダイアログ表示する
            }
            public AdbProgressData(string message, Exception ex)
            {
                Message = message + ":" + ex.Message;
                Ex = ex;
                IsError = true;
                IsRequireDialog = true;   //エラーの場合は大抵ダイアログ表示する
            }
        }

        /// <summary>
        /// ADBコマンドへのパス
        /// </summary>
        public string AdbCommandPath { get; }

        public AdbCommand(string adbCommandPath)
        {
            AdbCommandPath = adbCommandPath;
        }

        /// <summary>
        /// キャンセル指定なしADBコマンド起動
        /// </summary>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Task<string[]> Cmd(Device device, params string[] args)
        {
            return Cmd(CancellationToken.None, device, args);
        }

        /// <summary>
        /// キャンセル可能ADBコマンド起動
        /// 標準出力結果を行ごとの配列にして返却
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Task<string[]> Cmd(CancellationToken ct, Device device, params string[] args)
        {
            if (device == null) throw new Exception("端末が選択されていません");
            if (device.Type == "unauthorized") throw new Exception("端末接続が許可されていません。端末上からこのPCからの接続を許可してください");

            var tcs = new TaskCompletionSource<string[]>();
            Task.Run(() => {
                var pro = new Process();

                if (ct != CancellationToken.None)
                {
                    ct.Register(() => {
                        tcs.TrySetCanceled(ct);
                        try
                        {
                            pro.Kill();
                            pro.Dispose();
                        }
                        catch
                        {

                        }
                    });
                }

                var app = pro.StartInfo;
                app.FileName = AdbCommandPath ?? "adb";

                app.Arguments = string.Join(" ", args);

                if (device != Device.None)
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
            },ct);

            return tcs.Task;
        }

        /// <summary>
        /// 接続されているAndroidデバイス一覧取得
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public Task<IEnumerable<Device>> GetDeviceList(IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("デバイス一覧取得", progress, async () => {
                var result = await Cmd(Device.None, "devices");
                return result.Skip(1).Select(s => s.Split(new[] { "\t" }, StringSplitOptions.None)).Select(s => new Device(s[0], s[1]));
            });
        }

        /// <summary>
        /// 選択したAndroidデバイスにインストールされたサードパーティー製アプリのパッケージ名一覧取得
        /// </summary>
        /// <param name="device"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public Task<IEnumerable<PackageData>> GetPackageList(Device device, IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("パッケージリスト取得", progress, async () => {
                var result = await Cmd(device, "shell pm list package", "-3","-f");
                return result.Skip(0).Select(s => s.Split(new[] { ":","=" }, StringSplitOptions.None)).Select(s => new PackageData(s[2],s[1]));
            });
        }

        /// <summary>
        /// 指定したAndroidデバイスから指定したパッケージ名のアプリのアンインストール
        /// </summary>
        /// <param name="device"></param>
        /// <param name="package"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 指定したAndroidデバイスから、指定したパッケージ名のアプリの起動
        /// </summary>
        /// <param name="device"></param>
        /// <param name="package"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 指定したAndroidデバイスから、指定したパッケージ名のアプリの強制終了を試行
        /// </summary>
        /// <param name="device"></param>
        /// <param name="package"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public Task<bool> StopPackage(Device device, PackageData package, IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("アプリケーション停止処理", progress, async() => {
                var result = await Cmd(device, "shell am force-stop", package.Name);
                return true;
            });
        }

        /// <summary>
        /// 指定したAndroidデバイスから、指定したパッケージ名のAPKファイルをダウンロー</summary>
        /// <param name="device"></param>
        /// <param name="package"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public Task<bool> DownloadApk(Device device, PackageData package, string path,IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("APKダウンロード", progress, async () => {
                var result = await Cmd(device, "pull",package.ApkPath,path);
                var isSuccess = result.LastOrDefault()?.IndexOf("pulled") >= 0;
                if (isSuccess) return true;
                throw new Exception();
            });
        }

        /// <summary>
        /// 指定したAndroidデバイスに、filePathのAPKファイルをインストール
        /// </summary>
        /// <param name="device"></param>
        /// <param name="filePath"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public Task<bool> InstallPackage(Device device, string filePath, IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("インストール", progress, async () => {
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120)))
                {
                    var result = await Cmd(cts.Token,device, "install", "-r", filePath);

                    var isSuccess = result.Any(s => s.IndexOf("Success") >= 0);
                    if (isSuccess) return true;
                    throw new Exception();
                }
            },true);
        }

        /// <summary>
        /// 指定したAndroidデバイスに、IPアドレスによる接続を試行
        /// </summary>
        /// <param name="device"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
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

        /// <summary>
        /// IPアドレスによる接続を切断
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public Task<bool> DisconnectIp(IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("IP切断", progress, async () => {
                await Cmd(Device.None, "disconnect");
                return true;
            });
        }

        /// <summary>
        /// 指定したAndroidデバイスの強制シャットダウンを試行
        /// </summary>
        /// <param name="device"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public Task<bool> Shutdown(Device device, IProgress<AdbProgressData> progress)
        {
            return ProgressWrap("シャットダウン", progress, async () => {
                await Cmd(device, "shell", "reboot -p");
                return true;
            });
        }

        /// <summary>
        /// 指定したAndroidデバイスの強制再起動を試行
        /// </summary>
        /// <param name="device"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public Task<bool> Reboot(Device device,Progress<AdbProgressData> progress)
        {
            return ProgressWrap("端末再起動", progress, async () => {
                await Cmd(device, "shell", "reboot");
                return true;
            });
        }

        /// <summary>
        /// 進捗管理用ラップメソッド
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="progress"></param>
        /// <param name="proc"></param>
        /// <returns></returns>
        private async Task<T> ProgressWrap<T>(string name,IProgress<AdbProgressData>progress, Func<Task<T>> proc,bool isRequireCompleteDialog = false)
        {
            try
            {
                progress.Report(new AdbProgressData(name + "開始"));
                var result  = await proc.Invoke();
                progress.Report(new AdbProgressData(name + "成功",false,true,isRequireCompleteDialog));
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GuiAdb
{
    public class AdbCommand
    {
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

            app.Arguments = string.Join(" ",args);

            if (device != null)
            {
                app.Arguments = $"-s {device.Name} " + app.Arguments;
            }

            app.CreateNoWindow = true;
            app.UseShellExecute = false;
            app.RedirectStandardOutput = true;

            pro.Start();

            var output = pro.StandardOutput.ReadToEnd();

            tcs.TrySetResult(output.Replace("\r\r", "\r").Replace("\n", "\r\n").Replace("\r\r", "\r").Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries));

            return tcs.Task;
        }

        public async Task<IEnumerable<Device>> GetDeviceList()
        {
            try
            {
                var result = await Cmd(null, "devices");
                return result.Skip(1).Select(s => s.Split(new[]{
                    "\t"
                }, StringSplitOptions.None)).Select(s => new Device(s[0], s[1]));
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<PackageData>> GetPackageList(Device device)
        {
            try
            {
                var result = await Cmd(device, "shell pm list package", "-3");
                return result.Skip(0).Select(s => s.Split(new[]{
                    ":"
                }, StringSplitOptions.None)).Select(s => new PackageData(s[1]));
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UnInstallPackage(Device device, PackageData package)
        {
            try
            {
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
                {
                    var result = await Cmd(cts.Token, device, "uninstall", package.Name);
              
                    var isSuccess = result.FirstOrDefault()?.IndexOf("Success") >= 0;

                    if (isSuccess) return true;

                    Console.WriteLine(string.Join("\n", result));
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> LunchPackage(Device device, PackageData package)
        {
            try
            {
                var dumpResult = await Cmd(device, "shell", $"\"pm dump {package.Name} | grep -A 2 android.intent.action.MAIN | head -2 | tail -1\"");

                var packageActivityName = dumpResult[1].Split(new[]{
                    " "
                }, StringSplitOptions.RemoveEmptyEntries).ElementAtOrDefault(1);

                var result = await Cmd(device, "shell am start", "-n", packageActivityName);

                var isSuccess = result.FirstOrDefault()?.IndexOf("Success") >= 0 || result.FirstOrDefault()?.IndexOf("Starting") >= 0;

                if (isSuccess) return true;

                Console.WriteLine(string.Join("\n", result));
                return false;
            }
            catch
            {
                
            }
            return false;
        }

        public async Task<bool> InstallPackage(Device device, string filePath)
        {
            var result = await Cmd(device, "install", "-r",filePath);

            var isSuccess = result.Any(s => s.IndexOf("Success") >= 0);
            if (isSuccess) return true;
            Console.WriteLine(string.Join("\n", result));
            return false;
        }

        public async Task<bool> ConnectIp(Device device)
        {
            var result = await Cmd(device, "shell", "\"ifconfig wlan0 | grep 'inet addr:' | sed -e 's/^.*inet addr://' -e 's/ .*//'\"");
            var tcpIpResult = await Cmd(device, "tcpip", "5555");

            var connectResult =  await Cmd(device, "connect", $"{result[0]}:5555");

            if (connectResult.Any(s => s.IndexOf("connected to") >= 0)) return true;

            return false;
        }

        public Task DisconnectIp()
        {
            return Cmd(null, "disconnect");
        }
    }
}
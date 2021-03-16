using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GitGooWee
{
    public static class Terminal
    {
        public static string Send(string cmd)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return SendWindows(cmd);
            }
            
            return SendShell(cmd);
            }

        public static string SendWindows(string cmd)
        {
            return "";
        }

        public static string SendShell(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/zsh",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }


}

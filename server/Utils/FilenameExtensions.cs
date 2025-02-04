using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Utils
{
    public static class FilenameExtensions
    {
        public static string ResolveUnixHome(this string path)
        {
            if (!IsWindows() && path.Contains("~/"))
            {                
                var home = Environment.GetEnvironmentVariable("HOME");
                path = path.Replace("~/", "");
                path = Path.Combine(home, path);                
            }

            return path;
        }

        public static bool IsWindows() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }
}

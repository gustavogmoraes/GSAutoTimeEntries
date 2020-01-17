using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace GSAutoTimeEntries.Utils
{
    public static class SeleniumHelper
    {
        /// <summary>
        /// Gets chroem driver version.
        /// </summary>
        /// <param name="chromeDriverPath">Null means solution base directory</param>
        /// <param name="chromeDriverFileName"></param>
        /// <returns></returns>
        public static string GetChromeDriverVersion(string chromeDriverPath = null, string chromeDriverFileName = "chromedriver.exe")
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(chromeDriverPath ?? AppDomain.CurrentDomain.BaseDirectory, chromeDriverFileName),
                    Arguments = "-v",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                return TrateStringVersao(line);
            }

            return null;
        }

        public static string GetChromeBrowserVersion()
        {
            var path = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe", "", null) ??
                       Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe", "", null);

            return path != null ? FileVersionInfo.GetVersionInfo(path.ToString()).FileVersion : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath">Null means solution base directory</param>
        /// <returns></returns>
        public static bool CheckIfChromeDriverIsPresent(string directoryPath = null)
        {
            return File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chromedriver.exe"));
        }

        public static void AtualizeChromeDriver(string versaoChrome)
        {
            using (var client = new HttpClient())
            using (var response = client.GetAsync("https://chromedriver.chromium.org/downloads").Result)
            using (var content = response.Content)
            {
                var result = content.ReadAsStringAsync().Result;
                var substring = result.Substring(result.IndexOf("If you are using", StringComparison.Ordinal), result.IndexOf("For older version of Chrome", StringComparison.Ordinal));
                var splitted = substring.Split(new[] { "please download" }, StringSplitOptions.None);

                var links = splitted.Where((t, i) => i != 0)
                    .Select(t => t.ObtenhaStringEntre(@"a href=", " ").Replace("\"", string.Empty))
                    .ToList();

                var linkEspecifico = links.FirstOrDefault(x => x.Contains($"{versaoChrome}."));
                var linkDownload = linkEspecifico?.Replace("index.html?path=", string.Empty) + "chromedriver_win32.zip";

                WebHelper.BaixeArquivo(linkDownload, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chromedriver_win32.zip"));
                CompressionHelper.DescompacteZip(AppDomain.CurrentDomain.BaseDirectory, "chromedriver_win32.zip");
            }
        }

        private static string TrateStringVersao(string versao)
        {
            return versao.Split('.').First().Replace("ChromeDriver", string.Empty).Trim();
        }
    }
}

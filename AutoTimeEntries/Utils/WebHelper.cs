using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GSAutoTimeEntries.Utils
{
    public static class WebHelper
    {
        public static void BaixeArquivo(string urlArquivo, string caminhoNoComputador)
        {
            using (var httpClient = new HttpClient())
            using (var stream = httpClient.GetStreamAsync(urlArquivo).Result)
            using (var outputStream = new FileStream(caminhoNoComputador, FileMode.Create))
            {
                stream.CopyToAsync(outputStream).Wait();
            }
        }
    }
}

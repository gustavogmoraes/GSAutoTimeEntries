using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace GSAutoTimeEntries.Utils
{
    public static class AssistenteHorario
    {
        public static DateTime ObtenhaHorarioAtualBrasiliaViaApi()
        {
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync(@"https://timezonedb.com/references/get-time-zone");

            return new DateTime?() ?? DateTime.Now;
        }

        public static bool VerifiqueConexao()
        {
            return false;
        }
    }
}

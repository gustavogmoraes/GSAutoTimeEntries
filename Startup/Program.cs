using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Iniciando aplicação... aguarde");

            string url = @"http://localhost:44344/api/Lancamento/InicieApp";

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = 0;

            try
            {
                request.GetResponse();
            }
            catch
            {
                return;
            }
        }
    }
}

using GSAutoTimeEntries.Servicos;
using GSAutoTimeEntries.UI;
using GSAutoTimeEntries.Utils;
using GSAutoTimeEntriesWebApi.Objetos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GSAutoTimeEntries.Controllers
{
    // Se for usar a fetchApi para fazer as requests, habilitar 'Cors' em 'Startup.config' e adicionar o attribute 'EnableCors' 
    // nos Controllers desejados colocando os endpoints de onde se espera as request, caso seja pra habilitar todos colocar '*'

    [EnableCors(origins: @"*", headers: "*", methods: "*")]
    public class ExposeController : ApiController
    {
        [HttpPost]
        public void RestaureJanela()
        {
            GerenciadorDeForms.Obtenha<frmPrincipal>().CrossThreadInvoke(x => x.RestaureJanela());
        }

        [HttpPost]
        public string ExecuteLancamento(JObject parametros)
        {
            string result;

            try
            {
                var configuracaoRedmine = JsonConvert.DeserializeObject<ConfiguracaoRedmine>(parametros["Configuracao"].ToString());
                var pagina = parametros["PaginaDaTarefa"].ToString();
                var lancamentos = JsonConvert.DeserializeObject<List<Lancamento>>(parametros["Lancamentos"].ToString());

                using (var servicoDeConfiguracao = new ServicoDeConfiguracao())
                {
                    var configuracaoServer = servicoDeConfiguracao.ObtenhaConfiguracao();
                    configuracaoServer.ConfiguracaoRedmine.Usuario = configuracaoRedmine.Usuario;
                    configuracaoServer.ConfiguracaoRedmine.Senha = configuracaoRedmine.Senha;

                    using (var servicoDeLancamento = new ServicoDeLancamento(configuracaoServer))
                    {
                        servicoDeLancamento.RealizeLancamento(pagina, lancamentos);
                        result = "Great Success!";
                    }
                }
            }
            catch
            {
                result = "Error!";
            }
            finally
            {
                GerenciadorDeProgresso.Apague();
            }

            return result;
        }
    }
}

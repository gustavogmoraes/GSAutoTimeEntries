using LancamentoHoras.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace LancamentoHorasWebApi.Controllers
{
    public class LancamentoController : ApiController
    {
        [HttpPost]
        [Route("api/Lancamento/InicieApp")]
        public void InicieApp()
        {
            LancamentoHoras.Program.Main();
        }

        [HttpGet]
        [Route("api/Lancamento/MostreFormPrincipal")]
        public void MostreFormPrincipal()
        {
            var form = GerenciadorDeForms.Obtenha<frmPrincipal>();
            form.CrossThreadInvoke(form.RestaurarJanela);

            return;
        }
    }
}

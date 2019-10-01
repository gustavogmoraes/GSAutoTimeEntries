using GSAutoTimeEntries.Utils;
using System;
using System.Collections.Generic;

namespace GSAutoTimeEntriesWebApi.Objetos
{
    [Serializable]
    public class Dados
    {
        public PersistenceList<KeyValuePair<DateTime, Lancamento>> LancamentosRealizados { get; set; }

        public bool RegistroDePontoJaFoiRecuperadoHoje { get; set; }

        public Ponto RegistroDePontoDeHoje { get; set; }

        public bool LancamentoJaFoiRealizado { get; set; }

        public Lancamento LancamentoDeHoje { get; set; }

        public DateTime UltimoBloqueio { get; set; }

        public DateTime UltimoDesbloqueio { get; set; }

        public Dados()
        {
            LancamentosRealizados = new PersistenceList<KeyValuePair<DateTime, Lancamento>>();
        }
    }
}

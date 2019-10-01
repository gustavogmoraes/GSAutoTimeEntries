using System;

namespace GSAutoTimeEntriesWebApi.Objetos
{
    [Serializable]
    public class Configuracao
    {
        public string NomeUtilizador { get; set; }

        public bool OcultarNavegador { get; set; }

        public ConfiguracaoRedmine ConfiguracaoRedmine { get; set; }

        public ConfiguracaoAutotendimento ConfiguracaoAutotendimento { get; set; }

        public ConfiguraoLancamentoDiario ConfiguraoLancamentoDiario { get; set; }
    }
}

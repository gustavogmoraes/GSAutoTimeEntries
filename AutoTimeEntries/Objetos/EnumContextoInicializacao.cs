using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSAutoTimeEntriesWebApi.Objetos
{
    public enum EnumContextoInicializacao
    {
        NORMAL,
        INICIAR_SERVICO_LANCAMENTO_DIARIO,
        RETRY,
        APLICACAO_JA_ABERTA
    }
}

using System;
using System.Collections.Generic;

namespace GSAutoTimeEntriesWebApi.Objetos
{
    public class Ponto
    {
        public string DiaDaSemana { get; set; }

        public string Data { get; set; }

        public List<TimeSpan> Marcacoes { get; set; }
    }
}

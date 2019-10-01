using System;
using System.Collections.Generic;

namespace GSAutoTimeEntriesWebApi.Objetos
{
    public class Ponto
    {
        public string Data { get; set; }

        public List<TimeSpan> HorariosDasBatidas { get; set; }
    }
}

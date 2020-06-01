
using System;
using System.Collections.Generic;
using System.Linq;
using GSAutoTimeEntries.Utils;

namespace GSAutoTimeEntriesWebApi.Objetos
{
    public class Lancamento
    {
        // Não tirar, o LiteDB precisa desse Id
        public Guid Id { get; set; }

        public bool Dispensado { get; set; }

        public DateTime Data { get; set; }

        private List<TimeSpan> _batidas;
        public List<TimeSpan> Batidas
        {
            get => _batidas;
            set
            {
                if(value != null)
                {
                    _batidas = value;

                    CalculeHoras();
                }
            }
        }

        public double Horas { get; set; }

        public string Comentario { get; set; }

        public string TipoAtividade { get; set; }

        public string LinkAtividade { get; set; }

        public bool ExatoOuNaoTrabalhado { get; set; }

        public bool NaoCalcular { get; set; }

        public string DiaDaSemana { get; set; }

        public void CalculeHoras()
        {
            if (NaoCalcular) return;
                Horas = 0;
            // Número de batidas par, logo está correto
            // Se entrou, tem que sair
            if (_batidas.Count % 2 == 0)
            {
                for (int i = _batidas.Count - 1; i > -1; i -= 2)
                {
                    Horas += (_batidas[i] - _batidas[i - 1]).ArredondeParaUmQuartoDeHora().TotalHours;
                }
            }
            else Horas = 0;
        }
    }
}

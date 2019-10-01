using System;
using System.Threading;

namespace GSAutoTimeEntries.Utils
{
    public class AssistenteDeDigitacao
    {
        public event EventHandler Idled = delegate { };

        public int MilissegundosDeEspera { get; set; }

        private Timer _timer { get; set; }

        public AssistenteDeDigitacao(int milissegundosDeEspera = 650)
        {
            MilissegundosDeEspera = milissegundosDeEspera;
            _timer =
                new Timer(p =>
                {
                    Idled(this, EventArgs.Empty);
                });
        }

        public void TextChanged()
        {
            _timer.Change(MilissegundosDeEspera, Timeout.Infinite);
        }
    }
}

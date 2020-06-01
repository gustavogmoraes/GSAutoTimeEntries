using GSAutoTimeEntries.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace GSAutoTimeEntriesWebApi.Objetos
{
    public class Validador
    {
        public bool ValideSeArquivoEhRegistroDePonto(string caminhoArquivoDePonto, bool exibirProgresso = true)
        {
            if(exibirProgresso)
                GerenciadorDeProgresso.AtualizeProgressBar(75, "Validando relatório");

            var linhas = File.ReadAllLines(caminhoArquivoDePonto);
            if (linhas[0].Contains("dataTextBox") && linhas[0].Contains("marcacoesTextBox"))
            {
                return true;
            }

            MessageBox.Show("Arquivo invalido ou fora de formato, emita de acordo com o layout!");
            return false;
        }

        public bool ValideSeLinkDeTarefaEhValido(string link)
        {
            if (string.IsNullOrEmpty(link))
            {
                return false;
            }

            link = link.Trim();
            return link.StartsWith("http") &&
                   link.Contains("redmine") &&
                   link.Contains("issues") &&
                   char.IsDigit(link.Last());
        }

        public void Dispose()
        {

        }

        public bool ValideSeAtividadeEhValida(string p)
        {
            if (!string.IsNullOrEmpty(p))
            {
                return true;
            }

            return false;
        }

        public List<int> ValideLancamentos(List<Lancamento> list)
        {
            var listaRetorno = new List<int>();

            for (int i = 0; i < list.Count; i++)
            {
                if (!Valide(list[i]))
                {
                    listaRetorno.Add(i);
                }
            }

            return listaRetorno;
        }

        private bool Valide(Lancamento lancamento)
        {
            if (lancamento.Horas < 0 || lancamento.Comentario == null || !ValideSeLinkDeTarefaEhValido(lancamento.LinkAtividade))
            {
                return false;
            }

            return true;
        }
    }
}

using System;
using System.IO;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using GSAutoTimeEntriesWebApi.Objetos;
using GSAutoTimeEntries.Utils;
using Newtonsoft.Json;
using File = System.IO.File;

namespace GSAutoTimeEntries.Servicos
{
    public class ServicoDeConfiguracao : IDisposable
    {
        public string DiretorioRaiz = AppDomain.CurrentDomain.BaseDirectory;
        public const string NOME_ARQUIVO = "Configuracao.json";

        public static readonly string StartupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

        public static readonly string NomeAtalho = @"GSAutoTimeEntries.lnk";

        public static readonly string CaminhoDoAtalho = $@"{StartupFolder}/{NomeAtalho}";

        private void ExecuteFluxoDeletarAtalho()
        {
            if (System.IO.File.Exists(CaminhoDoAtalho))
                System.IO.File.Delete(CaminhoDoAtalho);
        }

        private void ExecuteFluxoCriacaoAtalho()
        {
            var shell = new WshShell();
            var atalho = (IWshShortcut)shell.CreateShortcut(CaminhoDoAtalho);

            atalho.Description = "Atalho de inicialização da aplicação GSAutoTimeEntries. " +
                                 "Se você deletar esse atalho a aplicação não será mais iniciada a cada login.";
            atalho.WorkingDirectory = Application.StartupPath;
            atalho.TargetPath = Application.ExecutablePath;
            atalho.Arguments = "-inicieServicoLancamento";
            atalho.Save();
        }

        public void SalveConfiguracao(Configuracao configuracao)
        {
            if (configuracao != null)
            {
                var caminho = DiretorioRaiz + NOME_ARQUIVO;
                if (System.IO.File.Exists(caminho))
                {
                    System.IO.File.Delete(caminho);
                }

                configuracao.ConfiguracaoRedmine.Senha = configuracao.ConfiguracaoRedmine.Senha.CriptografeBase64();
                configuracao.ConfiguracaoAutotendimento.Senha = configuracao.ConfiguracaoAutotendimento.Senha.CriptografeBase64();

                var jsonConfiguracao = JsonConvert.SerializeObject(configuracao);
                
                File.WriteAllText(DiretorioRaiz + NOME_ARQUIVO, jsonConfiguracao);

                if (configuracao.ConfiguraoLancamentoDiario != null)
                {
                    if(configuracao.ConfiguraoLancamentoDiario.Habilitar)
                    {
                        ExecuteFluxoCriacaoAtalho();
                        ServicoDeLancamentoDiario.Inicie();
                    }
                    else
                    {
                        ExecuteFluxoDeletarAtalho();
                        ServicoDeLancamentoDiario.Pare();
                    }
                }
            }
        }

        public Configuracao ObtenhaConfiguracao()
        {
            var caminho = DiretorioRaiz + NOME_ARQUIVO;
            if (File.Exists(caminho))
            {
                var conteudo = File.ReadAllText(caminho);
                if (conteudo != null && !string.IsNullOrEmpty(conteudo))
                {
                    var configuracao = JsonConvert.DeserializeObject<Configuracao>(conteudo);

                    configuracao.ConfiguracaoRedmine.Senha = configuracao.ConfiguracaoRedmine.Senha.DescriptografeBase64();
                    configuracao.ConfiguracaoAutotendimento.Senha = configuracao.ConfiguracaoAutotendimento.Senha.DescriptografeBase64();

                    return configuracao;
                }
            }

            return null;
        }

        public void ApagueConfiguracao()
        {
            var caminho = DiretorioRaiz + NOME_ARQUIVO;
            if (File.Exists(caminho))
            {
                File.Delete(caminho);
            }
        }

        public void Dispose()
        {

        }
    }
}

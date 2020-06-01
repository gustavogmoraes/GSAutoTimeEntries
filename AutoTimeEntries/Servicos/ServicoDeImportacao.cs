using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using IWshRuntimeLibrary;
using GSAutoTimeEntriesWebApi.Objetos;
using GSAutoTimeEntries.UI;
using GSAutoTimeEntries.Utils;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;

namespace GSAutoTimeEntries.Servicos
{
    public class ServicoDeImportacao : IDisposable
    {
        public List<Ponto> ImporteRegistroDePonto(string caminhoArquivoXlsx)
        {
            const string colunaDiaDaSemana = "Dia";
            const string colunaData = "Data";
            const string colunaMarcacoes = "MarcacoesStr";

            var dataTable = GetDataTableFromExcel(caminhoArquivoXlsx);

            return dataTable?.Rows.OfType<DataRow>()
                .Where(row => !string.IsNullOrEmpty(row[colunaDiaDaSemana].ToString()))
                .Select(row => new Ponto
                {
                    DiaDaSemana = row[colunaDiaDaSemana].ToString(),
                    Data = row[colunaData].ToString(),
                    Marcacoes = ObtenhaMarcacoes(row[colunaMarcacoes].ToString())
                })
                .ToList();
        }

        public static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var pck = new ExcelPackage())
            {
                using (var stream = System.IO.File.OpenRead(path))
                {
                    pck.Load(stream);
                }

                var ws = pck.Workbook.Worksheets.First();

                var tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : $"Column {firstRowCell.Start.Column}");
                }

                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    var row = tbl.Rows.Add();

                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }

                return tbl;
            }
        }

        private List<TimeSpan> ObtenhaMarcacoes(string dados)
        {
            if (string.IsNullOrEmpty(dados))
            {
                return null;
            }

            return dados.Trim()
            .Split(new[] {" "}, StringSplitOptions.None)
            .Select(x =>
            {
                if (x.Length <= 5) return ObtenhaBatida(x);
                if (x.Last() == ':') return ObtenhaBatida(x.Remove(x.Length - 1, 1));

                var valores = x.Split(' ');
                return ObtenhaBatida(valores[0].Remove(5, 1));
            })
            .ToList();
        }

        private TimeSpan ObtenhaBatida(string batida)
        {
            if (string.IsNullOrEmpty(batida))
            {
                return TimeSpan.Zero;
            }

            var splitted = batida.Split(':');
            return new TimeSpan(Convert.ToInt32(splitted[0]), Convert.ToInt32(splitted[1]), 0);
        }

        public List<Lancamento> ObtenhaLancamentosPorImportacao(string caminhoArquivoDePonto, DateTime dataInicio, DateTime dataFim, bool exibirProgresso)
        {
            if (exibirProgresso)
            {
                GerenciadorDeProgresso.AtualizeProgressBar(80, "Processando registros");
            }

            Thread.Sleep(1000);

            var listaDeLancamentos = new List<Lancamento>();
            var listaDePontos = ImporteRegistroDePonto(caminhoArquivoDePonto);

            return listaDePontos.Select(x => new Lancamento
            {
                DiaDaSemana = x.DiaDaSemana,
                Data = x.Data.ParaDateTime(false),
                Batidas = x.Marcacoes == null 
                        ? new List<TimeSpan>() 
                        : x.Marcacoes.OrderBy(y => y).ToList()
            }).ToList();
        }

        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private string ObtenhaDataDoLancamento(string dataImportada)
        {
            var splitted = dataImportada.Split('/');

            return splitted[2] + "-" + splitted[1] + "-" + splitted[0];
        }

        public void Dispose()
        {

        }
    }
}

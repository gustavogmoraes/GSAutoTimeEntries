using GSAutoTimeEntriesWebApi.Objetos;
using GSAutoTimeEntries.Servicos;
using GSAutoTimeEntries.Utils;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSAutoTimeEntries.UI
{
    public partial class frmLancamentoCorretivo : MetroForm
    {
        #region Construtores

        private void Comum()
        {
            InitializeComponent();

            InicieAssistentes();
        }

        public frmLancamentoCorretivo()
        {
            Comum();
        }

        public frmLancamentoCorretivo(DatasRetry retry)
        {
            Comum();
            Visible = false;

            dtpDataInicio.Value = retry.DataInicio;
            dtpDataFim.Value = retry.DataFim;

            button1_Click(button1, null);
        }

        #endregion

        #region Propriedades

        private AssistenteDeDigitacao AssistenteDeDigitacao { get; set; }

        private AssistenteDeDigitacao AssistenteDeDigitacaoLinkTarefa { get; set; }

        private Validador _validador { get; set; }

        private Validador Validador { get { return _validador ?? (_validador = new Validador()); } }

        private Configuracao Configuracoes
        {
            get
            {
                using (var servicoDeConfiguracao = new ServicoDeConfiguracao())
                {
                    return servicoDeConfiguracao.ObtenhaConfiguracao();
                }
            }
        }

        #endregion

        #region Métodos

        private void InicieAssistentes()
        {
            // Comentário
            AssistenteDeDigitacao = new AssistenteDeDigitacao();
            AssistenteDeDigitacao.Idled += Assistant_Idled;

            // Link da tarefa
            AssistenteDeDigitacaoLinkTarefa = new AssistenteDeDigitacao();
            AssistenteDeDigitacaoLinkTarefa.Idled += Assistant_Idled_LinkTarefa;
        }

        private void AtualizeLancamento(int indiceDaLinha, Lancamento lancamento)
        {
            dataGridView1["colunaHoras", indiceDaLinha].Value = lancamento.Horas;
        }

        private void InsiraLancamentos(IList<Lancamento> listaDeLancamentos)
        {
            dataGridView1.Rows.Clear();

            var atividade = cbAtividade.Text;
            var comentario = txtComentario.Text;

            var contagemDeBatidasMaxima = listaDeLancamentos.Select(x => x.Batidas.Count).Max();
            for (int i = 0; i < contagemDeBatidasMaxima; i++)
            {
                var indexParaInsert = dataGridView1.Columns["colunaHoras"].Index;
                var colunaBatidas = new DataGridViewColumn(dataGridView1.Columns["colunaHoras"].CellTemplate)
                {
                    Name = $"colunaBatida{indexParaInsert}",
                    HeaderText = indexParaInsert % 2 == 0
                               ? $"Saída {indexParaInsert}"
                               : $"Entrada {indexParaInsert}",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
                };

                dataGridView1.Columns.Insert(indexParaInsert, colunaBatidas);
            }

            foreach (var lancamento in listaDeLancamentos)
            {
                var linha = new DataGridViewRow();
                linha.Cells.Add(new DataGridViewTextBoxCell {Value = lancamento.Data });
                foreach (var batida in lancamento.Batidas)
                {
                    var valor = batida.ToString();
                    linha.Cells.Add(new DataGridViewTextBoxCell { Value = valor.Remove(valor.Length - 3, 3) });
                }

                if (lancamento.Batidas.Count < contagemDeBatidasMaxima)
                {
                    var diference = contagemDeBatidasMaxima - lancamento.Batidas.Count;
                    for (int i = 0; i < diference; i++)
                    {
                        linha.Cells.Add(new DataGridViewTextBoxCell { Value = string.Empty });
                    }
                }

                var ignoreList = new[] { "Data", "Batidas", "_batidas", "ExatoOuNaoTrabalhado", "LinkAtividade", "TipoLancamento" };
                var props = typeof(Lancamento).GetProperties().Where(x => !ignoreList.Contains(x.Name));
                var cells = props.Select(x => new DataGridViewTextBoxCell { Value = x.GetValue(lancamento) }).ToList();
                cells.Add(new DataGridViewTextBoxCell { Value = lancamento.ExatoOuNaoTrabalhado.ConvertaBooleano() });

                if (cells.Count > 4)
                {
                    cells.RemoveAt(cells.Count - 1);
                }

                linha.Cells.AddRange(cells.ToArray());

                dataGridView1.Rows.Add(linha);
            }

            ExecuteFluxoParaHabilitarLancamento();
        }

        private List<Lancamento> ObtenhaLancamentosNaTela()
        {
            var listaDeLancamentos = new List<Lancamento>();
            for (var i = 0; i < dataGridView1.Rows.Count; i++)
            {
                listaDeLancamentos.Add(ObtenhaLancamentoNaLinhaX(i));
            }

            return listaDeLancamentos;
        }

        private Lancamento ObtenhaLancamentoNaLinhaX(int indiceDaLinha)
        {
            var lancamento = new Lancamento
            {
                Data = dataGridView1["colunaData", indiceDaLinha].Value.ToString(),
                Comentario = (dataGridView1["colunaComentario", indiceDaLinha].Value ?? string.Empty).ToString(),
                LinkAtividade = txtLinkTarefa.Text,
                TipoAtividade = (dataGridView1["colunaAtividade", indiceDaLinha].Value ?? string.Empty).ToString(),
                Horas = Convert.ToDouble(dataGridView1["colunaHoras", indiceDaLinha].Value ?? 0),
                ExatoOuNaoTrabalhado = dataGridView1["colunaExatoOuNaoTrabalhado", indiceDaLinha].Value != null
                                     ? dataGridView1["colunaExatoOuNaoTrabalhado", indiceDaLinha].Value.ToString().ConvertaBooleano()
                                     : false,
                NaoCalcular = true
            };

            var indexDaColunaHoras = dataGridView1.Columns["colunaHoras"].Index;
            if (indexDaColunaHoras != 0)
            {
                lancamento.Batidas = new List<TimeSpan>();
                for (int i = 1; i < indexDaColunaHoras; i++)
                {
                    var valor = dataGridView1[i, indiceDaLinha].Value.ToString();
                    if (valor != null && !string.IsNullOrEmpty(valor))
                        lancamento.Batidas.Add(ObtenhaBatida(valor));
                }
            }

            lancamento.CalculeHoras();
            return lancamento;
        }

        private TimeSpan ObtenhaBatida(string batida)
        {
            var splitted = batida.Split(':');

            return new TimeSpan(Convert.ToInt32(splitted[0]), Convert.ToInt32(splitted[1]), 0);
        }

        private void ExecuteFluxoParaHabilitarLancamento()
        {
            var linkTarefa = Validador.ValideSeLinkDeTarefaEhValido(txtLinkTarefa.Text);
            var atividade = Validador.ValideSeAtividadeEhValida(cbAtividade.Text);

            var listaLancamento = ObtenhaLancamentosNaTela();
            var listaLancamentoInvalido = Validador.ValideLancamentos(listaLancamento);

            var contagemLancamentos = listaLancamento.Count;
            for (int i = 0; i < contagemLancamentos; i++)
            {
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = listaLancamento[i].ExatoOuNaoTrabalhado ? Color.CornflowerBlue : Color.White;
            }

            foreach (var lancamento in listaLancamentoInvalido)
            {
                var row = dataGridView1.Rows[lancamento];

                row.DefaultCellStyle.BackColor = row.DefaultCellStyle.BackColor != Color.White ? row.DefaultCellStyle.BackColor : Color.LightCoral;
            }

            var lancamentos = listaLancamentoInvalido.Count == 0;

            if (linkTarefa && atividade && lancamentos)
            {
                btnLancamento.Enabled = true;
            }
            else
            {
                btnLancamento.Enabled = false;
            }
        }

        #region Eventos

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();

            GerenciadorDeProgresso.Crie();
            Task.Run(() =>
            {
                using (var servicoDeLancamento = new ServicoDeLancamento(Configuracoes))
                {
                    var dataInicio = dtpDataInicio.Value;
                    var dataFim = dtpDataFim.Value;

                    var lancamentos = servicoDeLancamento.ObtenhaLancamentos(dataInicio, dataFim);
                    if (lancamentos != null && lancamentos.Count > 0)
                    {
                        GerenciadorDeProgresso.AtualizeProgressBar(95, "Inserindo registros para visualização");
                        Invoke((MethodInvoker)delegate
                        {
                            InsiraLancamentos(lancamentos);
                        });
                    }
                }

                var caminho = AppDomain.CurrentDomain.BaseDirectory + "retryFile.json";
                if (File.Exists(caminho))
                {
                    File.Delete(caminho);
                }

                GerenciadorDeProgresso.AtualizeProgressBar(100, "Concluído");
                GerenciadorDeProgresso.Apague();

                Invoke((MethodInvoker)delegate
                {
                    Visible = true;
                    WindowState = FormWindowState.Normal;
                    Show();
                    Activate();
                    Select();
                    Focus();
                });
            });
        }

        private void Assistant_Idled(object sender, EventArgs e)
        {
            Invoke(
                new MethodInvoker(() =>
                {
                    foreach (DataGridViewRow linha in dataGridView1.Rows)
                    {
                        linha.Cells["colunaComentario"].Value = txtComentario.Text;
                    }
                }));
        }

        private void Assistant_Idled_LinkTarefa(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(ExecuteFluxoParaHabilitarLancamento));
        }

        private void cbAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow linha in dataGridView1.Rows)
            {
                linha.Cells["colunaAtividade"].Value = cbAtividade.Text;
            }

            ExecuteFluxoParaHabilitarLancamento();
        }

        private void txtComentario_TextChanged(object sender, EventArgs e) => AssistenteDeDigitacao.TextChanged();

        private void btnLancamento_Click(object sender, EventArgs e)
        {
            var listaDeLancamentos = ObtenhaLancamentosNaTela();

            Hide();

            GerenciadorDeProgresso.Crie();
            Task.Run(() =>
            {
                using (var servicoDeLancamento = new ServicoDeLancamento(Configuracoes))
                {
                    GerenciadorDeProgresso.AtualizeProgressBar(10, "Iniciando lançamento");
                    servicoDeLancamento.RealizeLancamento(txtLinkTarefa.Text.Trim(), listaDeLancamentos);
                }

                GerenciadorDeProgresso.AtualizeProgressBar(100, "Concluído");
                GerenciadorDeProgresso.Apague();

                Invoke((MethodInvoker)delegate
                {
                    WindowState = FormWindowState.Normal;
                    Show();
                    Activate();
                    Select();
                    Focus();
                });
            });

            #region Comentado

            //var notification = new NotifyIcon()
            //{
            //    Visible = true,
            //    Icon = SystemIcons.Information,
            //    BalloonTipText = "Lançamento concluído!",
            //};

            //notification.ShowBalloonTip(5000);
            //Thread.Sleep(6000);

            //notification.Dispose();

            #endregion
        }

        private void txtLinkTarefa_TextChanged(object sender, EventArgs e) => AssistenteDeDigitacaoLinkTarefa.TextChanged();

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                using (var senderCell = dataGridView1[e.ColumnIndex, e.RowIndex])
                {
                    var valorCell = senderCell.Value != null 
                                  ? senderCell.Value.ToString()
                                  : string.Empty;
                    if (senderCell.OwningColumn.Name.StartsWith("colunaBatida") && !string.IsNullOrEmpty(valorCell))
                    {
                        var regex = @"^(?:[01]\d|2[0123]):(?:[012345]\d)$";
                        if (valorCell.Length > 6 || !Regex.IsMatch(valorCell, regex, RegexOptions.None))
                        {
                            MessageBox.Show(@"Formato inválido para a batida, o formato correto é no padrão hh:mm");
                            senderCell.Value = string.Empty;

                            return;
                        }

                        var lancamento = ObtenhaLancamentoNaLinhaX(senderCell.RowIndex);

                        lancamento.Batidas[senderCell.ColumnIndex - 1] = ObtenhaBatida(senderCell.Value.ToString());
                        lancamento.CalculeHoras();
                        AtualizeLancamento(senderCell.RowIndex, lancamento);
                    }
                }
            }

            ExecuteFluxoParaHabilitarLancamento();
        }

        private void frmLancamentoCorretivo_FormClosed(object sender, FormClosedEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                GerenciadorDeForms.Apague<frmLancamentoCorretivo>();
                GerenciadorDeForms.Obtenha<frmPrincipal>().AlterneBotaoCorretivo();
            });
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            GerenciadorDeForms.Apague<frmLancamentoCorretivo>();
            GerenciadorDeForms.Obtenha<frmPrincipal>().btnCorretivo.Enabled = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void txtLinkTarefa_Enter(object sender, EventArgs e) => txtLinkTarefaSub.AlterneEstilo();

        private void txtLinkTarefa_Leave(object sender, EventArgs e) => txtLinkTarefaSub.AlterneEstilo();

        private void txtComentario_Enter(object sender, EventArgs e) => txtComentarioSub.AlterneEstilo();

        private void txtComentario_Leave(object sender, EventArgs e) => txtComentarioSub.AlterneEstilo();

        private void cbAtividade_Enter(object sender, EventArgs e) => cbAtividadeSub.AlterneEstilo();

        private void cbAtividade_Leave(object sender, EventArgs e) => cbAtividadeSub.AlterneEstilo();

        private void txtLinkTarefaSub_Click(object sender, EventArgs e) => txtLinkTarefa.Focus();

        private void cbAtividadeSub_Click(object sender, EventArgs e) => cbAtividade.Focus();

        private void txtComentarioSub_Click(object sender, EventArgs e) => txtComentario.Focus();

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var oldSelectionMode = dataGridView1.SelectionMode;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];

            _lastClickedRowIndex = e.RowIndex;
            contextMenuGrid.Show(MousePosition);

            dataGridView1.SelectionMode = oldSelectionMode;
        }

        private int? _lastClickedRowIndex { get; set; }

        private void itemExcluir_Click(object sender, EventArgs e)
        {
            if (_lastClickedRowIndex != null)
                dataGridView1.Rows.RemoveAt(_lastClickedRowIndex.GetValueOrDefault());

            _lastClickedRowIndex = null;
        }

        #endregion

        #endregion

        private void Button2_Click(object sender, EventArgs e)
        {
            var dataInicio = dtpDataInicio.Value;
            var dataFim = dtpDataFim.Value;

            while (dataInicio < dataFim)
            {
                if (dataInicio.DayOfWeek != DayOfWeek.Saturday &&
                    dataInicio.DayOfWeek != DayOfWeek.Sunday)
                {
                    var data = dataInicio.ToString("dd/MM/yyyy");
                    if (!dataGridView1.Rows.OfType<DataGridViewRow>().ToList()
                        .Any(x => x.Cells[0].Value.ToString().Equals(data)))
                    {
                        dataGridView1.Rows.Add(data);
                    }
                }

                dataInicio = dataInicio.AddDays(1);
            }

            ExecuteFluxoParaHabilitarLancamento();
        }

        private void ContextMenuGrid_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void ItemDuplicar_Click(object sender, EventArgs e)
        {
            if (_lastClickedRowIndex != null)
            {
                var index = _lastClickedRowIndex.GetValueOrDefault();
                dataGridView1.Rows.Insert(
                    index + 1,
                    dataGridView1[0, index].Value, dataGridView1[1, index].Value, dataGridView1[2, index].Value, dataGridView1[3, index].Value);
            }

            _lastClickedRowIndex = null;
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var selectedRows = dataGridView1.SelectedCells.OfType<DataGridViewCell>()
                                                .Select(x => dataGridView1.Rows[x.RowIndex]);
                selectedRows.ToList().ForEach(dataGridView1.Rows.Remove);
            }
        }
    }
}

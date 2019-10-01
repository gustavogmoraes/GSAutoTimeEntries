namespace GSAutoTimeEntries.UI
{
    partial class frmLancamentoCorretivo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colunaData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colunaHoras = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colunaAtividade = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colunaComentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colunaExatoOuNaoTrabalhado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbAtividade = new System.Windows.Forms.ComboBox();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLinkTarefa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLancamento = new System.Windows.Forms.Button();
            this.dtpDataInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpDataFim = new System.Windows.Forms.DateTimePicker();
            this.txtLinkTarefaSub = new System.Windows.Forms.Label();
            this.txtComentarioSub = new System.Windows.Forms.Label();
            this.cbAtividadeSub = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.contextMenuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemExcluir = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDuplicar = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(857, 145);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "Obter registro de ponto";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GrayText;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colunaData,
            this.colunaHoras,
            this.colunaAtividade,
            this.colunaComentario,
            this.colunaExatoOuNaoTrabalhado});
            this.dataGridView1.Location = new System.Drawing.Point(13, 279);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1012, 377);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridView1_KeyDown);
            // 
            // colunaData
            // 
            this.colunaData.HeaderText = "Data";
            this.colunaData.Name = "colunaData";
            this.colunaData.Width = 80;
            // 
            // colunaHoras
            // 
            this.colunaHoras.HeaderText = "Horas";
            this.colunaHoras.Name = "colunaHoras";
            this.colunaHoras.Width = 60;
            // 
            // colunaAtividade
            // 
            this.colunaAtividade.HeaderText = "Atividade";
            this.colunaAtividade.Items.AddRange(new object[] {
            "Alinhamento de Requisitos",
            "Análise",
            "Análise de viabilidade",
            "Apoio a outra equipe",
            "Autodesenvolvimento",
            "Design",
            "Extra-Projeto",
            "GCS - Liberação de versão",
            "GCS - Processo Artifactory",
            "GCS - Processo TeamCity",
            "Ginástica Laboral",
            "Implementação",
            "Ociosidade - Infraestrutura",
            "Planejamento / Acompanhamento",
            "Preparação de Ambiente",
            "Processo",
            "Processo - Criação de tarefas",
            "Processo - GCS",
            "Processo - CCA",
            "Processo - Validação Escopo",
            "Processo - Validação Help",
            "Processo - Validar apropriação",
            "Proposta de solução",
            "Requisito",
            "Retrabalho",
            "Retrab - Análise de viabilidade",
            "Retrab - Design",
            "Retrab - Implementação",
            "Retrab - Proposta de solução",
            "Retrab - Requisito",
            "Retrab - Teste",
            "Reunião",
            "Revisão - Design",
            "Revisão - Implementação",
            "Revisão - Proposta de Solução",
            "Revisão - Requisitos",
            "Suporte",
            "Suporte - Externo",
            "Suporte - Interno ",
            "Teste",
            "Treinamento",
            "Trabalho"});
            this.colunaAtividade.Name = "colunaAtividade";
            this.colunaAtividade.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colunaAtividade.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colunaAtividade.Width = 200;
            // 
            // colunaComentario
            // 
            this.colunaComentario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colunaComentario.HeaderText = "Comentário";
            this.colunaComentario.Name = "colunaComentario";
            // 
            // colunaExatoOuNaoTrabalhado
            // 
            this.colunaExatoOuNaoTrabalhado.HeaderText = "";
            this.colunaExatoOuNaoTrabalhado.Name = "colunaExatoOuNaoTrabalhado";
            this.colunaExatoOuNaoTrabalhado.Visible = false;
            // 
            // cbAtividade
            // 
            this.cbAtividade.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cbAtividade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAtividade.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAtividade.ForeColor = System.Drawing.Color.White;
            this.cbAtividade.FormattingEnabled = true;
            this.cbAtividade.Items.AddRange(new object[] {
            "Alinhamento de Requisitos",
            "Análise",
            "Análise de viabilidade",
            "Apoio a outra equipe",
            "Autodesenvolvimento",
            "Design",
            "Extra-Projeto",
            "GCS - Liberação de versão",
            "GCS - Processo Artifactory",
            "GCS - Processo TeamCity",
            "Ginástica Laboral",
            "Implementação",
            "Ociosidade - Infraestrutura",
            "Planejamento / Acompanhamento",
            "Preparação de Ambiente",
            "Processo",
            "Processo - Criação de tarefas",
            "Processo - GCS",
            "Processo - CCA",
            "Processo - Validação Escopo",
            "Processo - Validação Help",
            "Processo - Validar apropriação",
            "Proposta de solução",
            "Requisito",
            "Retrabalho",
            "Retrab - Análise de viabilidade",
            "Retrab - Design",
            "Retrab - Implementação",
            "Retrab - Proposta de solução",
            "Retrab - Requisito",
            "Retrab - Teste",
            "Reunião",
            "Revisão - Design",
            "Revisão - Implementação",
            "Revisão - Proposta de Solução",
            "Revisão - Requisitos",
            "Suporte",
            "Suporte - Externo",
            "Suporte - Interno ",
            "Teste",
            "Treinamento",
            "Trabalho"});
            this.cbAtividade.Location = new System.Drawing.Point(14, 223);
            this.cbAtividade.Margin = new System.Windows.Forms.Padding(4);
            this.cbAtividade.Name = "cbAtividade";
            this.cbAtividade.Size = new System.Drawing.Size(318, 25);
            this.cbAtividade.TabIndex = 3;
            this.cbAtividade.SelectedIndexChanged += new System.EventHandler(this.cbAtividade_SelectedIndexChanged);
            this.cbAtividade.Enter += new System.EventHandler(this.cbAtividade_Enter);
            this.cbAtividade.Leave += new System.EventHandler(this.cbAtividade_Leave);
            // 
            // txtComentario
            // 
            this.txtComentario.BackColor = System.Drawing.Color.White;
            this.txtComentario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtComentario.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComentario.ForeColor = System.Drawing.Color.Black;
            this.txtComentario.Location = new System.Drawing.Point(367, 229);
            this.txtComentario.Margin = new System.Windows.Forms.Padding(4);
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(398, 18);
            this.txtComentario.TabIndex = 4;
            this.txtComentario.TextChanged += new System.EventHandler(this.txtComentario_TextChanged);
            this.txtComentario.Enter += new System.EventHandler(this.txtComentario_Enter);
            this.txtComentario.Leave += new System.EventHandler(this.txtComentario_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 195);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Atividade";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(363, 206);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Comentário";
            // 
            // txtLinkTarefa
            // 
            this.txtLinkTarefa.BackColor = System.Drawing.Color.White;
            this.txtLinkTarefa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLinkTarefa.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLinkTarefa.ForeColor = System.Drawing.Color.Black;
            this.txtLinkTarefa.Location = new System.Drawing.Point(14, 161);
            this.txtLinkTarefa.Margin = new System.Windows.Forms.Padding(4);
            this.txtLinkTarefa.Name = "txtLinkTarefa";
            this.txtLinkTarefa.Size = new System.Drawing.Size(549, 18);
            this.txtLinkTarefa.TabIndex = 7;
            this.txtLinkTarefa.TextChanged += new System.EventHandler(this.txtLinkTarefa_TextChanged);
            this.txtLinkTarefa.Enter += new System.EventHandler(this.txtLinkTarefa_Enter);
            this.txtLinkTarefa.Leave += new System.EventHandler(this.txtLinkTarefa_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(11, 135);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Link da tarefa no Redmine";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(10, 35);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Registro de ponto";
            // 
            // btnLancamento
            // 
            this.btnLancamento.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnLancamento.Enabled = false;
            this.btnLancamento.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLancamento.Location = new System.Drawing.Point(857, 664);
            this.btnLancamento.Margin = new System.Windows.Forms.Padding(4);
            this.btnLancamento.Name = "btnLancamento";
            this.btnLancamento.Size = new System.Drawing.Size(168, 24);
            this.btnLancamento.TabIndex = 12;
            this.btnLancamento.Text = "Efetuar Lançamento";
            this.btnLancamento.UseVisualStyleBackColor = false;
            this.btnLancamento.Click += new System.EventHandler(this.btnLancamento_Click);
            // 
            // dtpDataInicio
            // 
            this.dtpDataInicio.CalendarFont = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataInicio.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataInicio.Location = new System.Drawing.Point(24, 79);
            this.dtpDataInicio.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDataInicio.Name = "dtpDataInicio";
            this.dtpDataInicio.Size = new System.Drawing.Size(320, 27);
            this.dtpDataInicio.TabIndex = 13;
            // 
            // dtpDataFim
            // 
            this.dtpDataFim.CalendarFont = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataFim.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataFim.Location = new System.Drawing.Point(714, 79);
            this.dtpDataFim.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDataFim.Name = "dtpDataFim";
            this.dtpDataFim.Size = new System.Drawing.Size(320, 27);
            this.dtpDataFim.TabIndex = 14;
            // 
            // txtLinkTarefaSub
            // 
            this.txtLinkTarefaSub.AutoSize = true;
            this.txtLinkTarefaSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLinkTarefaSub.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtLinkTarefaSub.Location = new System.Drawing.Point(11, 172);
            this.txtLinkTarefaSub.Name = "txtLinkTarefaSub";
            this.txtLinkTarefaSub.Size = new System.Drawing.Size(562, 15);
            this.txtLinkTarefaSub.TabIndex = 40;
            this.txtLinkTarefaSub.Text = "_________________________________________________________________________________" +
    "______________________________";
            this.txtLinkTarefaSub.Click += new System.EventHandler(this.txtLinkTarefaSub_Click);
            // 
            // txtComentarioSub
            // 
            this.txtComentarioSub.AutoSize = true;
            this.txtComentarioSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComentarioSub.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtComentarioSub.Location = new System.Drawing.Point(364, 243);
            this.txtComentarioSub.Name = "txtComentarioSub";
            this.txtComentarioSub.Size = new System.Drawing.Size(412, 15);
            this.txtComentarioSub.TabIndex = 41;
            this.txtComentarioSub.Text = "_________________________________________________________________________________" +
    "";
            this.txtComentarioSub.Click += new System.EventHandler(this.txtComentarioSub_Click);
            // 
            // cbAtividadeSub
            // 
            this.cbAtividadeSub.AutoSize = true;
            this.cbAtividadeSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAtividadeSub.ForeColor = System.Drawing.Color.SteelBlue;
            this.cbAtividadeSub.Location = new System.Drawing.Point(12, 243);
            this.cbAtividadeSub.Name = "cbAtividadeSub";
            this.cbAtividadeSub.Size = new System.Drawing.Size(332, 15);
            this.cbAtividadeSub.TabIndex = 42;
            this.cbAtividadeSub.Text = "_________________________________________________________________";
            this.cbAtividadeSub.Click += new System.EventHandler(this.cbAtividadeSub_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.SteelBlue;
            this.label5.Location = new System.Drawing.Point(362, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(339, 30);
            this.label5.TabIndex = 43;
            this.label5.Text = "--------------------------------------->";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(22, 59);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 17);
            this.label6.TabIndex = 44;
            this.label6.Text = "Data de início";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(718, 59);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 17);
            this.label7.TabIndex = 45;
            this.label7.Text = "Data fim";
            // 
            // contextMenuGrid
            // 
            this.contextMenuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemExcluir,
            this.itemDuplicar});
            this.contextMenuGrid.Name = "contextMenuGrid";
            this.contextMenuGrid.ShowImageMargin = false;
            this.contextMenuGrid.ShowItemToolTips = false;
            this.contextMenuGrid.Size = new System.Drawing.Size(94, 48);
            this.contextMenuGrid.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuGrid_Opening);
            // 
            // itemExcluir
            // 
            this.itemExcluir.Name = "itemExcluir";
            this.itemExcluir.Size = new System.Drawing.Size(93, 22);
            this.itemExcluir.Text = "Excluir";
            this.itemExcluir.Click += new System.EventHandler(this.itemExcluir_Click);
            // 
            // itemDuplicar
            // 
            this.itemDuplicar.Name = "itemDuplicar";
            this.itemDuplicar.Size = new System.Drawing.Size(93, 22);
            this.itemDuplicar.Text = "Duplicar";
            this.itemDuplicar.Click += new System.EventHandler(this.ItemDuplicar_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(857, 114);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 25);
            this.button2.TabIndex = 46;
            this.button2.Text = "Preencher Datas";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // frmLancamentoCorretivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1046, 703);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbAtividade);
            this.Controls.Add(this.cbAtividadeSub);
            this.Controls.Add(this.txtComentarioSub);
            this.Controls.Add(this.txtLinkTarefa);
            this.Controls.Add(this.txtLinkTarefaSub);
            this.Controls.Add(this.dtpDataFim);
            this.Controls.Add(this.dtpDataInicio);
            this.Controls.Add(this.btnLancamento);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmLancamentoCorretivo";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLancamentoCorretivo_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbAtividade;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLinkTarefa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLancamento;
        private System.Windows.Forms.DateTimePicker dtpDataInicio;
        private System.Windows.Forms.DateTimePicker dtpDataFim;
        private System.Windows.Forms.Label txtLinkTarefaSub;
        private System.Windows.Forms.Label txtComentarioSub;
        private System.Windows.Forms.Label cbAtividadeSub;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ContextMenuStrip contextMenuGrid;
        private System.Windows.Forms.ToolStripMenuItem itemExcluir;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem itemDuplicar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colunaData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colunaHoras;
        private System.Windows.Forms.DataGridViewComboBoxColumn colunaAtividade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colunaComentario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colunaExatoOuNaoTrabalhado;
    }
}


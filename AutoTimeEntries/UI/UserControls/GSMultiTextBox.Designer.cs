namespace GSAutoTimeEntries.UI.UserControls
{
    partial class GSMultiTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.pnlHoras = new System.Windows.Forms.Panel();
            this.lblHoras = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.lblTextHoras = new MetroFramework.Controls.MetroLabel();
            this.txtInputBox = new System.Windows.Forms.TextBox();
            this.txtLink = new MetroFramework.Controls.MetroTextBox();
            this.cbAtividade = new MetroFramework.Controls.MetroComboBox();
            this.cbLinkAtividade = new MetroFramework.Controls.MetroComboBox();
            this.btnVoltarComboLink = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // btnRemover
            // 
            this.btnRemover.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRemover.Location = new System.Drawing.Point(641, 1);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(32, 30);
            this.btnRemover.TabIndex = 108;
            this.btnRemover.Text = "-";
            this.btnRemover.UseVisualStyleBackColor = true;
            this.btnRemover.Visible = false;
            this.btnRemover.Click += new System.EventHandler(this.BtnRemover_Click_1);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdicionar.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicionar.Location = new System.Drawing.Point(676, 1);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(32, 30);
            this.btnAdicionar.TabIndex = 107;
            this.btnAdicionar.Text = "+";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.BtnAdicionar_Click_1);
            // 
            // pnlHoras
            // 
            this.pnlHoras.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pnlHoras.BackColor = System.Drawing.Color.SkyBlue;
            this.pnlHoras.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHoras.Location = new System.Drawing.Point(3, 4);
            this.pnlHoras.Name = "pnlHoras";
            this.pnlHoras.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.pnlHoras.Size = new System.Drawing.Size(132, 25);
            this.pnlHoras.TabIndex = 109;
            // 
            // lblHoras
            // 
            this.lblHoras.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblHoras.AutoSize = true;
            this.lblHoras.Location = new System.Drawing.Point(142, 9);
            this.lblHoras.Name = "lblHoras";
            this.lblHoras.Size = new System.Drawing.Size(26, 19);
            this.lblHoras.TabIndex = 110;
            this.lblHoras.Text = "0.0";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(218, 8);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(48, 19);
            this.metroLabel2.TabIndex = 111;
            this.metroLabel2.Text = "----->";
            // 
            // lblTextHoras
            // 
            this.lblTextHoras.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTextHoras.AutoSize = true;
            this.lblTextHoras.Location = new System.Drawing.Point(172, 9);
            this.lblTextHoras.Name = "lblTextHoras";
            this.lblTextHoras.Size = new System.Drawing.Size(41, 19);
            this.lblTextHoras.TabIndex = 114;
            this.lblTextHoras.Text = "horas";
            // 
            // txtInputBox
            // 
            this.txtInputBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInputBox.Location = new System.Drawing.Point(676, 2);
            this.txtInputBox.Name = "txtInputBox";
            this.txtInputBox.Size = new System.Drawing.Size(31, 29);
            this.txtInputBox.TabIndex = 116;
            this.txtInputBox.Text = "0.0";
            this.txtInputBox.Visible = false;
            this.txtInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtInputBox_KeyDown);
            // 
            // txtLink
            // 
            // 
            // 
            // 
            this.txtLink.CustomButton.Image = null;
            this.txtLink.CustomButton.Location = new System.Drawing.Point(118, 1);
            this.txtLink.CustomButton.Name = "";
            this.txtLink.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtLink.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtLink.CustomButton.TabIndex = 1;
            this.txtLink.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtLink.CustomButton.UseSelectable = true;
            this.txtLink.CustomButton.Visible = false;
            this.txtLink.Lines = new string[0];
            this.txtLink.Location = new System.Drawing.Point(272, 5);
            this.txtLink.MaxLength = 32767;
            this.txtLink.Name = "txtLink";
            this.txtLink.PasswordChar = '\0';
            this.txtLink.PromptText = "Link da atividade";
            this.txtLink.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtLink.SelectedText = "";
            this.txtLink.SelectionLength = 0;
            this.txtLink.SelectionStart = 0;
            this.txtLink.ShortcutsEnabled = true;
            this.txtLink.Size = new System.Drawing.Size(140, 23);
            this.txtLink.TabIndex = 117;
            this.txtLink.UseSelectable = true;
            this.txtLink.WaterMark = "Link da atividade";
            this.txtLink.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtLink.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLink.TextChanged += new System.EventHandler(this.TxtLink_TextChanged);
            this.txtLink.Enter += new System.EventHandler(this.TxtLink_Enter);
            this.txtLink.Leave += new System.EventHandler(this.TxtLink_Leave);
            // 
            // cbAtividade
            // 
            this.cbAtividade.DropDownWidth = 250;
            this.cbAtividade.FormattingEnabled = true;
            this.cbAtividade.ItemHeight = 23;
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
            this.cbAtividade.Location = new System.Drawing.Point(444, 1);
            this.cbAtividade.Name = "cbAtividade";
            this.cbAtividade.PromptText = "Tipo de atividade";
            this.cbAtividade.Size = new System.Drawing.Size(191, 29);
            this.cbAtividade.TabIndex = 119;
            this.cbAtividade.UseSelectable = true;
            this.cbAtividade.SelectedIndexChanged += new System.EventHandler(this.CbAtividade_SelectedIndexChanged);
            // 
            // cbLinkAtividade
            // 
            this.cbLinkAtividade.DropDownWidth = 400;
            this.cbLinkAtividade.FormattingEnabled = true;
            this.cbLinkAtividade.ItemHeight = 23;
            this.cbLinkAtividade.Items.AddRange(new object[] {
            ">>> Entrar com link manualmente <<<"});
            this.cbLinkAtividade.Location = new System.Drawing.Point(272, 1);
            this.cbLinkAtividade.Name = "cbLinkAtividade";
            this.cbLinkAtividade.PromptText = "Atividade";
            this.cbLinkAtividade.Size = new System.Drawing.Size(166, 29);
            this.cbLinkAtividade.TabIndex = 120;
            this.cbLinkAtividade.UseSelectable = true;
            this.cbLinkAtividade.DropDown += new System.EventHandler(this.CbLinkAtividade_DropDown);
            this.cbLinkAtividade.SelectedIndexChanged += new System.EventHandler(this.CbLinkAtividade_SelectedIndexChanged);
            this.cbLinkAtividade.DropDownClosed += new System.EventHandler(this.CbLinkAtividade_DropDownClosed);
            this.cbLinkAtividade.Click += new System.EventHandler(this.CbLinkAtividade_Click);
            this.cbLinkAtividade.Leave += new System.EventHandler(this.CbLinkAtividade_Leave);
            // 
            // btnVoltarComboLink
            // 
            this.btnVoltarComboLink.Location = new System.Drawing.Point(414, 5);
            this.btnVoltarComboLink.Name = "btnVoltarComboLink";
            this.btnVoltarComboLink.Size = new System.Drawing.Size(24, 23);
            this.btnVoltarComboLink.TabIndex = 121;
            this.btnVoltarComboLink.Text = "<";
            this.btnVoltarComboLink.UseSelectable = true;
            this.btnVoltarComboLink.Click += new System.EventHandler(this.BtnVoltarComboLink_Click);
            // 
            // GSMultiTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cbLinkAtividade);
            this.Controls.Add(this.btnVoltarComboLink);
            this.Controls.Add(this.cbAtividade);
            this.Controls.Add(this.txtInputBox);
            this.Controls.Add(this.lblTextHoras);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.lblHoras);
            this.Controls.Add(this.pnlHoras);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.txtLink);
            this.Name = "GSMultiTextBox";
            this.Size = new System.Drawing.Size(741, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Panel pnlHoras;
        private MetroFramework.Controls.MetroLabel lblHoras;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel lblTextHoras;
        private System.Windows.Forms.TextBox txtInputBox;
        private MetroFramework.Controls.MetroTextBox txtLink;
        private MetroFramework.Controls.MetroComboBox cbAtividade;
        private MetroFramework.Controls.MetroComboBox cbLinkAtividade;
        private MetroFramework.Controls.MetroButton btnVoltarComboLink;
    }
}

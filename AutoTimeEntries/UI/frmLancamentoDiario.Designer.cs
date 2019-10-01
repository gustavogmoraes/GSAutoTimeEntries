namespace GSAutoTimeEntries.UI
{
    partial class frmLancamentoDiario
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
            this.lblHorario = new MetroFramework.Controls.MetroLabel();
            this.dtpHorario = new System.Windows.Forms.DateTimePicker();
            this.cbFormaLembrete = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.toggleHabilitar = new MetroFramework.Controls.MetroToggle();
            this.lblHabilitar = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // lblHorario
            // 
            this.lblHorario.AutoSize = true;
            this.lblHorario.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblHorario.Location = new System.Drawing.Point(229, 75);
            this.lblHorario.Name = "lblHorario";
            this.lblHorario.Size = new System.Drawing.Size(166, 25);
            this.lblHorario.TabIndex = 1;
            this.lblHorario.Text = "Horário do lembrete";
            // 
            // dtpHorario
            // 
            this.dtpHorario.CustomFormat = "hh:mm";
            this.dtpHorario.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHorario.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHorario.Location = new System.Drawing.Point(265, 103);
            this.dtpHorario.Name = "dtpHorario";
            this.dtpHorario.ShowUpDown = true;
            this.dtpHorario.Size = new System.Drawing.Size(81, 29);
            this.dtpHorario.TabIndex = 3;
            this.dtpHorario.ValueChanged += new System.EventHandler(this.DateTimePicker1_ValueChanged);
            // 
            // cbFormaLembrete
            // 
            this.cbFormaLembrete.FormattingEnabled = true;
            this.cbFormaLembrete.ItemHeight = 23;
            this.cbFormaLembrete.Items.AddRange(new object[] {
            "Horário fixo"});
            this.cbFormaLembrete.Location = new System.Drawing.Point(24, 103);
            this.cbFormaLembrete.Name = "cbFormaLembrete";
            this.cbFormaLembrete.Size = new System.Drawing.Size(174, 29);
            this.cbFormaLembrete.TabIndex = 4;
            this.cbFormaLembrete.UseSelectable = true;
            this.cbFormaLembrete.SelectedIndexChanged += new System.EventHandler(this.CbFormaLembrete_SelectedIndexChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(23, 75);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(131, 25);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Lembrete diário";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(255, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 43);
            this.button1.TabIndex = 6;
            this.button1.Text = "Salvar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // toggleHabilitar
            // 
            this.toggleHabilitar.AutoSize = true;
            this.toggleHabilitar.Location = new System.Drawing.Point(28, 192);
            this.toggleHabilitar.Name = "toggleHabilitar";
            this.toggleHabilitar.Size = new System.Drawing.Size(80, 17);
            this.toggleHabilitar.TabIndex = 7;
            this.toggleHabilitar.Text = "Off";
            this.toggleHabilitar.UseSelectable = true;
            // 
            // lblHabilitar
            // 
            this.lblHabilitar.AutoSize = true;
            this.lblHabilitar.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblHabilitar.Location = new System.Drawing.Point(23, 155);
            this.lblHabilitar.Name = "lblHabilitar";
            this.lblHabilitar.Size = new System.Drawing.Size(75, 25);
            this.lblHabilitar.TabIndex = 8;
            this.lblHabilitar.Text = "Habilitar";
            // 
            // frmLancamentoDiario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(423, 289);
            this.Controls.Add(this.lblHabilitar);
            this.Controls.Add(this.toggleHabilitar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.cbFormaLembrete);
            this.Controls.Add(this.dtpHorario);
            this.Controls.Add(this.lblHorario);
            this.MaximizeBox = false;
            this.Name = "frmLancamentoDiario";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow;
            this.Text = "Configurar lançamento diário";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLancamentoDiario_FormClosed);
            this.Load += new System.EventHandler(this.FrmLancamentoDiario_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel lblHorario;
        private System.Windows.Forms.DateTimePicker dtpHorario;
        private MetroFramework.Controls.MetroComboBox cbFormaLembrete;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.Button button1;
        private MetroFramework.Controls.MetroToggle toggleHabilitar;
        private MetroFramework.Controls.MetroLabel lblHabilitar;
    }
}
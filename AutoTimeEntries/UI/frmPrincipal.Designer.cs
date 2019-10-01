namespace GSAutoTimeEntries.UI
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemMostrarApp = new System.Windows.Forms.ToolStripMenuItem();
            this.lblBemVindo = new System.Windows.Forms.Label();
            this.lblStatus0 = new System.Windows.Forms.Label();
            this.btnRelatorios = new System.Windows.Forms.Button();
            this.btnDiario = new System.Windows.Forms.Button();
            this.btnCorretivo = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ucConfiguracao1 = new GSAutoTimeEntries.UI.UserControls.ucConfiguracao();
            this.ucBorders1 = new GSAutoTimeEntries.UI.UserControls.ucBorders();
            this.button4 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "GS Auto Time Entries";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemMostrarApp});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(116, 26);
            // 
            // itemMostrarApp
            // 
            this.itemMostrarApp.Name = "itemMostrarApp";
            this.itemMostrarApp.Size = new System.Drawing.Size(115, 22);
            this.itemMostrarApp.Text = "Mostrar App";
            this.itemMostrarApp.Click += new System.EventHandler(this.itemMostrarApp_Click);
            // 
            // lblBemVindo
            // 
            this.lblBemVindo.AutoSize = true;
            this.lblBemVindo.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBemVindo.ForeColor = System.Drawing.Color.Black;
            this.lblBemVindo.Location = new System.Drawing.Point(12, 33);
            this.lblBemVindo.Name = "lblBemVindo";
            this.lblBemVindo.Size = new System.Drawing.Size(132, 25);
            this.lblBemVindo.TabIndex = 3;
            this.lblBemVindo.Text = "Bem vindo {0},";
            // 
            // lblStatus0
            // 
            this.lblStatus0.AutoSize = true;
            this.lblStatus0.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus0.ForeColor = System.Drawing.Color.Black;
            this.lblStatus0.Location = new System.Drawing.Point(12, 67);
            this.lblStatus0.Name = "lblStatus0";
            this.lblStatus0.Size = new System.Drawing.Size(608, 21);
            this.lblStatus0.TabIndex = 4;
            this.lblStatus0.Text = "O sistema ainda não foi configurado, por favor utilize os ícones abaixo para conf" +
    "igurar.";
            // 
            // btnRelatorios
            // 
            this.btnRelatorios.BackColor = System.Drawing.Color.Gainsboro;
            this.btnRelatorios.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRelatorios.Enabled = false;
            this.btnRelatorios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelatorios.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelatorios.ForeColor = System.Drawing.Color.Black;
            this.btnRelatorios.Location = new System.Drawing.Point(436, 229);
            this.btnRelatorios.Name = "btnRelatorios";
            this.btnRelatorios.Size = new System.Drawing.Size(170, 155);
            this.btnRelatorios.TabIndex = 7;
            this.btnRelatorios.Text = "Relatórios";
            this.btnRelatorios.UseVisualStyleBackColor = false;
            this.btnRelatorios.Visible = false;
            // 
            // btnDiario
            // 
            this.btnDiario.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDiario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDiario.Enabled = false;
            this.btnDiario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiario.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiario.ForeColor = System.Drawing.Color.Black;
            this.btnDiario.Location = new System.Drawing.Point(224, 229);
            this.btnDiario.Name = "btnDiario";
            this.btnDiario.Size = new System.Drawing.Size(170, 155);
            this.btnDiario.TabIndex = 6;
            this.btnDiario.Text = "Diário";
            this.btnDiario.UseVisualStyleBackColor = false;
            this.btnDiario.Visible = false;
            this.btnDiario.Click += new System.EventHandler(this.BtnDiario_Click);
            // 
            // btnCorretivo
            // 
            this.btnCorretivo.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCorretivo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCorretivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorretivo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorretivo.ForeColor = System.Drawing.Color.Black;
            this.btnCorretivo.Location = new System.Drawing.Point(12, 229);
            this.btnCorretivo.Name = "btnCorretivo";
            this.btnCorretivo.Size = new System.Drawing.Size(170, 155);
            this.btnCorretivo.TabIndex = 5;
            this.btnCorretivo.Text = "Corretivo";
            this.btnCorretivo.UseVisualStyleBackColor = false;
            this.btnCorretivo.Click += new System.EventHandler(this.btnCorretivo_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(596, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Test Api";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Gainsboro;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(12, 91);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(170, 127);
            this.button2.TabIndex = 12;
            this.button2.Text = "Lançamento";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // ucConfiguracao1
            // 
            this.ucConfiguracao1.BackColor = System.Drawing.Color.White;
            this.ucConfiguracao1.Location = new System.Drawing.Point(801, 31);
            this.ucConfiguracao1.Name = "ucConfiguracao1";
            this.ucConfiguracao1.Size = new System.Drawing.Size(800, 375);
            this.ucConfiguracao1.TabIndex = 10;
            this.ucConfiguracao1.Toggle = false;
            // 
            // ucBorders1
            // 
            this.ucBorders1.BackColor = System.Drawing.Color.Gainsboro;
            this.ucBorders1.Location = new System.Drawing.Point(0, 0);
            this.ucBorders1.Name = "ucBorders1";
            this.ucBorders1.Size = new System.Drawing.Size(831, 30);
            this.ucBorders1.TabIndex = 9;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Gainsboro;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(224, 91);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(170, 127);
            this.button4.TabIndex = 14;
            this.button4.Text = "Correção";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(829, 396);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.ucConfiguracao1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ucBorders1);
            this.Controls.Add(this.btnRelatorios);
            this.Controls.Add(this.btnDiario);
            this.Controls.Add(this.btnCorretivo);
            this.Controls.Add(this.lblStatus0);
            this.Controls.Add(this.lblBemVindo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrincipal";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow;
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem itemMostrarApp;
        private System.Windows.Forms.Label lblBemVindo;
        private System.Windows.Forms.Label lblStatus0;
        public System.Windows.Forms.Button btnCorretivo;
        public System.Windows.Forms.Button btnDiario;
        public System.Windows.Forms.Button btnRelatorios;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private UserControls.ucBorders ucBorders1;
        private UserControls.ucConfiguracao ucConfiguracao1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button4;
    }
}
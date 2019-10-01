namespace GSAutoTimeEntries.UI
{
    partial class frmPopupDiario
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
            this.lblOque = new MetroFramework.Controls.MetroLabel();
            this.flpLancamentos = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTotalHoras = new MetroFramework.Controls.MetroLabel();
            this.lblTotal = new MetroFramework.Controls.MetroLabel();
            this.flpBatidas = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEfetuarLancamento = new System.Windows.Forms.Button();
            this.btnNaoLancar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblOque
            // 
            this.lblOque.AutoSize = true;
            this.lblOque.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblOque.Location = new System.Drawing.Point(22, 63);
            this.lblOque.Name = "lblOque";
            this.lblOque.Size = new System.Drawing.Size(192, 25);
            this.lblOque.TabIndex = 0;
            this.lblOque.Text = "O que você fez dia {0} ?";
            // 
            // flpLancamentos
            // 
            this.flpLancamentos.AutoScroll = true;
            this.flpLancamentos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpLancamentos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpLancamentos.Location = new System.Drawing.Point(24, 129);
            this.flpLancamentos.Name = "flpLancamentos";
            this.flpLancamentos.Size = new System.Drawing.Size(750, 276);
            this.flpLancamentos.TabIndex = 1;
            this.flpLancamentos.WrapContents = false;
            // 
            // lblTotalHoras
            // 
            this.lblTotalHoras.AutoSize = true;
            this.lblTotalHoras.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblTotalHoras.Location = new System.Drawing.Point(733, 63);
            this.lblTotalHoras.Name = "lblTotalHoras";
            this.lblTotalHoras.Size = new System.Drawing.Size(34, 25);
            this.lblTotalHoras.TabIndex = 2;
            this.lblTotalHoras.Text = "0.0";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblTotal.Location = new System.Drawing.Point(614, 63);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(121, 25);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Total de horas:";
            // 
            // flpBatidas
            // 
            this.flpBatidas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpBatidas.Location = new System.Drawing.Point(25, 91);
            this.flpBatidas.Name = "flpBatidas";
            this.flpBatidas.Size = new System.Drawing.Size(750, 32);
            this.flpBatidas.TabIndex = 4;
            // 
            // btnEfetuarLancamento
            // 
            this.btnEfetuarLancamento.BackColor = System.Drawing.Color.SkyBlue;
            this.btnEfetuarLancamento.Enabled = false;
            this.btnEfetuarLancamento.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEfetuarLancamento.Location = new System.Drawing.Point(586, 411);
            this.btnEfetuarLancamento.Name = "btnEfetuarLancamento";
            this.btnEfetuarLancamento.Size = new System.Drawing.Size(188, 32);
            this.btnEfetuarLancamento.TabIndex = 5;
            this.btnEfetuarLancamento.Text = "Ok";
            this.btnEfetuarLancamento.UseVisualStyleBackColor = false;
            this.btnEfetuarLancamento.Click += new System.EventHandler(this.BtnEfetuarLancamento_Click);
            // 
            // btnNaoLancar
            // 
            this.btnNaoLancar.BackColor = System.Drawing.Color.LightCoral;
            this.btnNaoLancar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNaoLancar.Location = new System.Drawing.Point(25, 411);
            this.btnNaoLancar.Name = "btnNaoLancar";
            this.btnNaoLancar.Size = new System.Drawing.Size(191, 32);
            this.btnNaoLancar.TabIndex = 6;
            this.btnNaoLancar.Text = "Dispensar lançamento";
            this.btnNaoLancar.UseVisualStyleBackColor = false;
            this.btnNaoLancar.Visible = false;
            this.btnNaoLancar.Click += new System.EventHandler(this.BtnNaoLancar_Click);
            // 
            // frmPopupDiario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.btnNaoLancar);
            this.Controls.Add(this.btnEfetuarLancamento);
            this.Controls.Add(this.flpBatidas);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblTotalHoras);
            this.Controls.Add(this.flpLancamentos);
            this.Controls.Add(this.lblOque);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "frmPopupDiario";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Lançamento Diário";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmPopupDiario_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel lblOque;
        private System.Windows.Forms.FlowLayoutPanel flpLancamentos;
        private MetroFramework.Controls.MetroLabel lblTotalHoras;
        private MetroFramework.Controls.MetroLabel lblTotal;
        private System.Windows.Forms.FlowLayoutPanel flpBatidas;
        private System.Windows.Forms.Button btnNaoLancar;
        public System.Windows.Forms.Button btnEfetuarLancamento;
    }
}
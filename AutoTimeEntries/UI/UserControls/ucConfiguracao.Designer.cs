namespace GSAutoTimeEntries.UI.UserControls
{
    partial class ucConfiguracao
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
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.txtSenhaSharepoint = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtUsuarioSharepoint = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtLinkLoginSharepoint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLinkSharepointSub = new System.Windows.Forms.Label();
            this.txtUsuarioSharepointSub = new System.Windows.Forms.Label();
            this.txtSenhaSharepointSub = new System.Windows.Forms.Label();
            this.txtSenhaRedmine = new System.Windows.Forms.TextBox();
            this.txtSenhaRedmineSub = new System.Windows.Forms.Label();
            this.txtUsuarioRedmine = new System.Windows.Forms.TextBox();
            this.txtUsuarioRedmineSub = new System.Windows.Forms.Label();
            this.txtLinkLoginRedmine = new System.Windows.Forms.TextBox();
            this.txtLinkLoginRedmineSub = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblRedmine = new System.Windows.Forms.Label();
            this.cbOcultarNavegador = new System.Windows.Forms.CheckBox();
            this.lblVisualizacao = new System.Windows.Forms.Label();
            this.btnApagarConfig = new System.Windows.Forms.Button();
            this.txtNomeUtilizador = new System.Windows.Forms.TextBox();
            this.txtNomeUtilizadorSub = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(30, 397);
            this.panel2.TabIndex = 1;
            this.panel2.Click += new System.EventHandler(this.panel2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-4, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "<<";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // timer
            // 
            this.timer.Interval = 10;
            // 
            // txtSenhaSharepoint
            // 
            this.txtSenhaSharepoint.BackColor = System.Drawing.Color.White;
            this.txtSenhaSharepoint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSenhaSharepoint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenhaSharepoint.ForeColor = System.Drawing.Color.Black;
            this.txtSenhaSharepoint.Location = new System.Drawing.Point(56, 164);
            this.txtSenhaSharepoint.Name = "txtSenhaSharepoint";
            this.txtSenhaSharepoint.PasswordChar = '*';
            this.txtSenhaSharepoint.Size = new System.Drawing.Size(212, 18);
            this.txtSenhaSharepoint.TabIndex = 36;
            this.txtSenhaSharepoint.Enter += new System.EventHandler(this.txtSenhaSharepoint_Enter);
            this.txtSenhaSharepoint.Leave += new System.EventHandler(this.txtSenhaSharepoint_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(54, 90);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 20);
            this.label13.TabIndex = 35;
            this.label13.Text = "Usuário";
            // 
            // txtUsuarioSharepoint
            // 
            this.txtUsuarioSharepoint.BackColor = System.Drawing.Color.White;
            this.txtUsuarioSharepoint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsuarioSharepoint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuarioSharepoint.ForeColor = System.Drawing.Color.Black;
            this.txtUsuarioSharepoint.Location = new System.Drawing.Point(56, 113);
            this.txtUsuarioSharepoint.Name = "txtUsuarioSharepoint";
            this.txtUsuarioSharepoint.Size = new System.Drawing.Size(212, 18);
            this.txtUsuarioSharepoint.TabIndex = 34;
            this.txtUsuarioSharepoint.Enter += new System.EventHandler(this.txtUsuarioSharepoint_Enter);
            this.txtUsuarioSharepoint.Leave += new System.EventHandler(this.txtUsuarioSharepoint_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(51, 39);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(97, 20);
            this.label14.TabIndex = 33;
            this.label14.Text = "Link p/ login";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(48, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(199, 25);
            this.label15.TabIndex = 32;
            this.label15.Text = "Autoatendimento LG";
            // 
            // txtLinkLoginSharepoint
            // 
            this.txtLinkLoginSharepoint.BackColor = System.Drawing.Color.White;
            this.txtLinkLoginSharepoint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLinkLoginSharepoint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLinkLoginSharepoint.ForeColor = System.Drawing.Color.Black;
            this.txtLinkLoginSharepoint.Location = new System.Drawing.Point(56, 62);
            this.txtLinkLoginSharepoint.Name = "txtLinkLoginSharepoint";
            this.txtLinkLoginSharepoint.Size = new System.Drawing.Size(212, 18);
            this.txtLinkLoginSharepoint.TabIndex = 31;
            this.txtLinkLoginSharepoint.Text = "https://login.lg.com.br/autenticacao/produtos/saaa/Principal2.aspx?c=lg";
            this.txtLinkLoginSharepoint.Enter += new System.EventHandler(this.txtLinkLoginSharepoint_Enter);
            this.txtLinkLoginSharepoint.Leave += new System.EventHandler(this.txtLinkLoginSharepoint_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(54, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 37;
            this.label2.Text = "Senha";
            // 
            // txtLinkSharepointSub
            // 
            this.txtLinkSharepointSub.AutoSize = true;
            this.txtLinkSharepointSub.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLinkSharepointSub.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtLinkSharepointSub.Location = new System.Drawing.Point(53, 72);
            this.txtLinkSharepointSub.Name = "txtLinkSharepointSub";
            this.txtLinkSharepointSub.Size = new System.Drawing.Size(222, 15);
            this.txtLinkSharepointSub.TabIndex = 38;
            this.txtLinkSharepointSub.Text = "___________________________________________";
            // 
            // txtUsuarioSharepointSub
            // 
            this.txtUsuarioSharepointSub.AutoSize = true;
            this.txtUsuarioSharepointSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuarioSharepointSub.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtUsuarioSharepointSub.Location = new System.Drawing.Point(53, 121);
            this.txtUsuarioSharepointSub.Name = "txtUsuarioSharepointSub";
            this.txtUsuarioSharepointSub.Size = new System.Drawing.Size(222, 15);
            this.txtUsuarioSharepointSub.TabIndex = 39;
            this.txtUsuarioSharepointSub.Text = "___________________________________________";
            // 
            // txtSenhaSharepointSub
            // 
            this.txtSenhaSharepointSub.AutoSize = true;
            this.txtSenhaSharepointSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenhaSharepointSub.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtSenhaSharepointSub.Location = new System.Drawing.Point(55, 172);
            this.txtSenhaSharepointSub.Name = "txtSenhaSharepointSub";
            this.txtSenhaSharepointSub.Size = new System.Drawing.Size(222, 15);
            this.txtSenhaSharepointSub.TabIndex = 41;
            this.txtSenhaSharepointSub.Text = "___________________________________________";
            // 
            // txtSenhaRedmine
            // 
            this.txtSenhaRedmine.BackColor = System.Drawing.Color.White;
            this.txtSenhaRedmine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSenhaRedmine.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenhaRedmine.ForeColor = System.Drawing.Color.Black;
            this.txtSenhaRedmine.Location = new System.Drawing.Point(323, 164);
            this.txtSenhaRedmine.Name = "txtSenhaRedmine";
            this.txtSenhaRedmine.PasswordChar = '*';
            this.txtSenhaRedmine.Size = new System.Drawing.Size(212, 18);
            this.txtSenhaRedmine.TabIndex = 47;
            this.txtSenhaRedmine.Enter += new System.EventHandler(this.txtSenhaRedmine_Enter);
            this.txtSenhaRedmine.Leave += new System.EventHandler(this.txtSenhaRedmine_Leave);
            // 
            // txtSenhaRedmineSub
            // 
            this.txtSenhaRedmineSub.AutoSize = true;
            this.txtSenhaRedmineSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenhaRedmineSub.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtSenhaRedmineSub.Location = new System.Drawing.Point(322, 172);
            this.txtSenhaRedmineSub.Name = "txtSenhaRedmineSub";
            this.txtSenhaRedmineSub.Size = new System.Drawing.Size(222, 15);
            this.txtSenhaRedmineSub.TabIndex = 51;
            this.txtSenhaRedmineSub.Text = "___________________________________________";
            // 
            // txtUsuarioRedmine
            // 
            this.txtUsuarioRedmine.BackColor = System.Drawing.Color.White;
            this.txtUsuarioRedmine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsuarioRedmine.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuarioRedmine.ForeColor = System.Drawing.Color.Black;
            this.txtUsuarioRedmine.Location = new System.Drawing.Point(323, 113);
            this.txtUsuarioRedmine.Name = "txtUsuarioRedmine";
            this.txtUsuarioRedmine.Size = new System.Drawing.Size(212, 18);
            this.txtUsuarioRedmine.TabIndex = 45;
            this.txtUsuarioRedmine.Enter += new System.EventHandler(this.txtUsuarioRedmine_Enter);
            this.txtUsuarioRedmine.Leave += new System.EventHandler(this.txtUsuarioRedmine_Leave);
            // 
            // txtUsuarioRedmineSub
            // 
            this.txtUsuarioRedmineSub.AutoSize = true;
            this.txtUsuarioRedmineSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuarioRedmineSub.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtUsuarioRedmineSub.Location = new System.Drawing.Point(320, 121);
            this.txtUsuarioRedmineSub.Name = "txtUsuarioRedmineSub";
            this.txtUsuarioRedmineSub.Size = new System.Drawing.Size(222, 15);
            this.txtUsuarioRedmineSub.TabIndex = 50;
            this.txtUsuarioRedmineSub.Text = "___________________________________________";
            // 
            // txtLinkLoginRedmine
            // 
            this.txtLinkLoginRedmine.BackColor = System.Drawing.Color.White;
            this.txtLinkLoginRedmine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLinkLoginRedmine.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLinkLoginRedmine.ForeColor = System.Drawing.Color.Black;
            this.txtLinkLoginRedmine.Location = new System.Drawing.Point(323, 62);
            this.txtLinkLoginRedmine.Name = "txtLinkLoginRedmine";
            this.txtLinkLoginRedmine.Size = new System.Drawing.Size(212, 18);
            this.txtLinkLoginRedmine.TabIndex = 42;
            this.txtLinkLoginRedmine.Text = "http://srv-redmine/redmine";
            this.txtLinkLoginRedmine.Enter += new System.EventHandler(this.txtLinkLoginRedmine_Enter);
            this.txtLinkLoginRedmine.Leave += new System.EventHandler(this.txtLinkLoginRedmine_Leave);
            // 
            // txtLinkLoginRedmineSub
            // 
            this.txtLinkLoginRedmineSub.AutoSize = true;
            this.txtLinkLoginRedmineSub.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLinkLoginRedmineSub.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtLinkLoginRedmineSub.Location = new System.Drawing.Point(320, 72);
            this.txtLinkLoginRedmineSub.Name = "txtLinkLoginRedmineSub";
            this.txtLinkLoginRedmineSub.Size = new System.Drawing.Size(222, 15);
            this.txtLinkLoginRedmineSub.TabIndex = 49;
            this.txtLinkLoginRedmineSub.Text = "___________________________________________";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(321, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 20);
            this.label6.TabIndex = 48;
            this.label6.Text = "Senha";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(321, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 20);
            this.label7.TabIndex = 46;
            this.label7.Text = "Usuário";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(318, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 20);
            this.label8.TabIndex = 44;
            this.label8.Text = "Link p/ login";
            // 
            // lblRedmine
            // 
            this.lblRedmine.AutoSize = true;
            this.lblRedmine.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedmine.ForeColor = System.Drawing.Color.Black;
            this.lblRedmine.Location = new System.Drawing.Point(315, 10);
            this.lblRedmine.Name = "lblRedmine";
            this.lblRedmine.Size = new System.Drawing.Size(90, 25);
            this.lblRedmine.TabIndex = 43;
            this.lblRedmine.Text = "Redmine";
            // 
            // cbOcultarNavegador
            // 
            this.cbOcultarNavegador.AutoSize = true;
            this.cbOcultarNavegador.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOcultarNavegador.ForeColor = System.Drawing.Color.Black;
            this.cbOcultarNavegador.Location = new System.Drawing.Point(575, 40);
            this.cbOcultarNavegador.Name = "cbOcultarNavegador";
            this.cbOcultarNavegador.Size = new System.Drawing.Size(151, 24);
            this.cbOcultarNavegador.TabIndex = 52;
            this.cbOcultarNavegador.Text = "Ocultar navegador";
            this.cbOcultarNavegador.UseVisualStyleBackColor = true;
            // 
            // lblVisualizacao
            // 
            this.lblVisualizacao.AutoSize = true;
            this.lblVisualizacao.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVisualizacao.ForeColor = System.Drawing.Color.Black;
            this.lblVisualizacao.Location = new System.Drawing.Point(565, 10);
            this.lblVisualizacao.Name = "lblVisualizacao";
            this.lblVisualizacao.Size = new System.Drawing.Size(87, 25);
            this.lblVisualizacao.TabIndex = 53;
            this.lblVisualizacao.Text = "Diversos";
            // 
            // btnApagarConfig
            // 
            this.btnApagarConfig.BackColor = System.Drawing.Color.Firebrick;
            this.btnApagarConfig.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApagarConfig.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnApagarConfig.Location = new System.Drawing.Point(575, 141);
            this.btnApagarConfig.Name = "btnApagarConfig";
            this.btnApagarConfig.Size = new System.Drawing.Size(151, 28);
            this.btnApagarConfig.TabIndex = 54;
            this.btnApagarConfig.Text = "Apagar configuração";
            this.btnApagarConfig.UseVisualStyleBackColor = false;
            this.btnApagarConfig.Click += new System.EventHandler(this.btnApagarConfig_Click);
            // 
            // txtNomeUtilizador
            // 
            this.txtNomeUtilizador.BackColor = System.Drawing.Color.White;
            this.txtNomeUtilizador.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNomeUtilizador.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeUtilizador.ForeColor = System.Drawing.Color.Black;
            this.txtNomeUtilizador.Location = new System.Drawing.Point(573, 95);
            this.txtNomeUtilizador.Name = "txtNomeUtilizador";
            this.txtNomeUtilizador.Size = new System.Drawing.Size(212, 18);
            this.txtNomeUtilizador.TabIndex = 55;
            this.txtNomeUtilizador.Enter += new System.EventHandler(this.txtNomeUtilizador_Enter);
            this.txtNomeUtilizador.Leave += new System.EventHandler(this.txtNomeUtilizador_Leave);
            // 
            // txtNomeUtilizadorSub
            // 
            this.txtNomeUtilizadorSub.AutoSize = true;
            this.txtNomeUtilizadorSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeUtilizadorSub.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtNomeUtilizadorSub.Location = new System.Drawing.Point(570, 103);
            this.txtNomeUtilizadorSub.Name = "txtNomeUtilizadorSub";
            this.txtNomeUtilizadorSub.Size = new System.Drawing.Size(202, 15);
            this.txtNomeUtilizadorSub.TabIndex = 57;
            this.txtNomeUtilizadorSub.Text = "_______________________________________";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(571, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 20);
            this.label4.TabIndex = 56;
            this.label4.Text = "Nome utilizador";
            // 
            // ucConfiguracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtNomeUtilizador);
            this.Controls.Add(this.txtNomeUtilizadorSub);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnApagarConfig);
            this.Controls.Add(this.lblVisualizacao);
            this.Controls.Add(this.cbOcultarNavegador);
            this.Controls.Add(this.txtSenhaRedmine);
            this.Controls.Add(this.txtSenhaRedmineSub);
            this.Controls.Add(this.txtUsuarioRedmine);
            this.Controls.Add(this.txtUsuarioRedmineSub);
            this.Controls.Add(this.txtLinkLoginRedmine);
            this.Controls.Add(this.txtLinkLoginRedmineSub);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblRedmine);
            this.Controls.Add(this.txtSenhaSharepoint);
            this.Controls.Add(this.txtSenhaSharepointSub);
            this.Controls.Add(this.txtUsuarioSharepoint);
            this.Controls.Add(this.txtUsuarioSharepointSub);
            this.Controls.Add(this.txtLinkLoginSharepoint);
            this.Controls.Add(this.txtLinkSharepointSub);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.Name = "ucConfiguracao";
            this.Size = new System.Drawing.Size(800, 375);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox txtSenhaSharepoint;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtUsuarioSharepoint;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtLinkLoginSharepoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtLinkSharepointSub;
        private System.Windows.Forms.Label txtUsuarioSharepointSub;
        private System.Windows.Forms.Label txtSenhaSharepointSub;
        private System.Windows.Forms.TextBox txtSenhaRedmine;
        private System.Windows.Forms.Label txtSenhaRedmineSub;
        private System.Windows.Forms.TextBox txtUsuarioRedmine;
        private System.Windows.Forms.Label txtUsuarioRedmineSub;
        private System.Windows.Forms.TextBox txtLinkLoginRedmine;
        private System.Windows.Forms.Label txtLinkLoginRedmineSub;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblRedmine;
        private System.Windows.Forms.CheckBox cbOcultarNavegador;
        private System.Windows.Forms.Label lblVisualizacao;
        private System.Windows.Forms.Button btnApagarConfig;
        private System.Windows.Forms.TextBox txtNomeUtilizador;
        private System.Windows.Forms.Label txtNomeUtilizadorSub;
        private System.Windows.Forms.Label label4;
    }
}

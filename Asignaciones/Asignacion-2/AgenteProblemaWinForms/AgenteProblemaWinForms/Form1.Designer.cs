namespace AgenteProblemaWinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnChat = new Button();
            btnDashboard = new Button();
            pnlContenido = new Panel();
            btnCerrar = new Button();
            SuspendLayout();
            // 
            // btnChat
            // 
            btnChat.Location = new Point(12, 12);
            btnChat.Name = "btnChat";
            btnChat.Size = new Size(75, 23);
            btnChat.TabIndex = 0;
            btnChat.Text = "Chat";
            btnChat.UseVisualStyleBackColor = true;
            btnChat.Click += btnChat_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.Location = new Point(93, 12);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(75, 23);
            btnDashboard.TabIndex = 1;
            btnDashboard.Text = "Dashboard";
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // pnlContenido
            // 
            pnlContenido.Location = new Point(12, 58);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(1014, 422);
            pnlContenido.TabIndex = 2;
            pnlContenido.Paint += pnlContenido_Paint;
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(174, 12);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(75, 23);
            btnCerrar.TabIndex = 3;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1038, 492);
            Controls.Add(btnCerrar);
            Controls.Add(pnlContenido);
            Controls.Add(btnDashboard);
            Controls.Add(btnChat);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnChat;
        private Button btnDashboard;
        private Panel pnlContenido;
        private Button btnCerrar;
    }
}

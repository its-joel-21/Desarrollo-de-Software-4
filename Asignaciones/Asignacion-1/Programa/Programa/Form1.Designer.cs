namespace Programa
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
            txtContenido = new TextBox();
            btnGuardar = new Button();
            btnCargar = new Button();
            btnLimpiar = new Button();
            btnEjecutar = new Button();
            SuspendLayout();
            // 
            // txtContenido
            // 
            txtContenido.Location = new Point(12, 84);
            txtContenido.Multiline = true;
            txtContenido.Name = "txtContenido";
            txtContenido.Size = new Size(344, 300);
            txtContenido.TabIndex = 1;
            txtContenido.TextChanged += txtContenido_TextChanged;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(64, 0, 64);
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(188, 30);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(81, 32);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCargar
            // 
            btnCargar.BackColor = Color.FromArgb(64, 0, 64);
            btnCargar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCargar.ForeColor = Color.White;
            btnCargar.Location = new Point(99, 30);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(81, 32);
            btnCargar.TabIndex = 5;
            btnCargar.Text = "Cargar";
            btnCargar.UseVisualStyleBackColor = false;
            btnCargar.Click += btnCargar_Click_1;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(64, 0, 64);
            btnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Location = new Point(276, 30);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(81, 32);
            btnLimpiar.TabIndex = 8;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEjecutar
            // 
            btnEjecutar.BackColor = Color.FromArgb(64, 0, 64);
            btnEjecutar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEjecutar.ForeColor = Color.White;
            btnEjecutar.Location = new Point(12, 30);
            btnEjecutar.Name = "btnEjecutar";
            btnEjecutar.Size = new Size(81, 32);
            btnEjecutar.TabIndex = 10;
            btnEjecutar.Text = "Ejecutar";
            btnEjecutar.UseVisualStyleBackColor = false;
            btnEjecutar.Click += btnEjecutar_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(369, 404);
            Controls.Add(btnEjecutar);
            Controls.Add(btnGuardar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnCargar);
            Controls.Add(txtContenido);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtContenido;
        private Button btnGuardar;
        private Button btnCargar;
        private Button btnLimpiar;
        private Button btnEjecutar;
    }
}

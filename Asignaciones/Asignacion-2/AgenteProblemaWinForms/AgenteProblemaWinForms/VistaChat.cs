using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.Agents.AI;

namespace AgenteProblemaWinForms
{
    public class VistaChat : UserControl
    {
        private AIAgent? _agente;

        private ComboBox cmbProveedor = null!;
        private TextBox txtConsulta = null!;
        private RichTextBox rtbConversacion = null!;
        private Label lblEstado = null!;
        private Panel indicadorEstado = null!;
        private Button btnCrear = null!;
        private Button btnEnviar = null!;

        private readonly Color colorFondo = Color.FromArgb(245, 247, 250);
        private readonly Color colorTarjeta = Color.White;
        private readonly Color colorTexto = Color.FromArgb(45, 55, 72);
        private readonly Color colorTextoSuave = Color.FromArgb(113, 128, 150);
        private readonly Color colorAcento = Color.FromArgb(74, 144, 164);
        private readonly Color colorBorde = Color.FromArgb(226, 232, 240);

        // evento que se dispara cuando el agente responde
        public event Action<string>? RespuestaRecibida;

        public VistaChat()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = colorFondo;
            this.Padding = new Padding(10);
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = colorFondo
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            layout.Controls.Add(CrearPanelTop(), 0, 0);
            layout.Controls.Add(CrearPanelConversacion(), 0, 1);
            layout.Controls.Add(CrearPanelInput(), 0, 2);

            this.Controls.Add(layout);
        }

        private Panel CrearPanelTop()
        {
            Panel p = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = colorTarjeta,
                Padding = new Padding(10),
                Margin = new Padding(0, 0, 0, 5)
            };
            p.Paint += (s, e) => PintarBordeRedondeado(p, e.Graphics);

            Label lblProv = new Label
            {
                Text = "Proveedor:",
                Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold),
                ForeColor = colorTexto,
                AutoSize = true,
                Location = new Point(10, 20)
            };

            cmbProveedor = new ComboBox
            {
                Width = 130,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                FlatStyle = FlatStyle.Flat,
                BackColor = colorFondo,
                Location = new Point(90, 17)
            };
            cmbProveedor.Items.AddRange(new object[] { "Groq" });
            cmbProveedor.SelectedIndex = 0;

            btnCrear = new Button
            {
                Text = "⚡ Crear Agente",
                Width = 130,
                Height = 28,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold),
                BackColor = colorAcento,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Location = new Point(235, 16)
            };
            btnCrear.FlatAppearance.BorderSize = 0;
            btnCrear.Click += BtnCrear_Click;
            btnCrear.Paint += (s, e) => PintarBotonRedondeado(btnCrear, e.Graphics);

            indicadorEstado = new Panel
            {
                Size = new Size(10, 10),
                Location = new Point(385, 25),
                BackColor = Color.Transparent
            };
            indicadorEstado.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var color = lblEstado.Text == "conectado"
                    ? Color.FromArgb(72, 187, 120)
                    : Color.FromArgb(245, 101, 101);
                e.Graphics.FillEllipse(new SolidBrush(color), 0, 0, 10, 10);
            };

            lblEstado = new Label
            {
                Text = "sin conexión",
                Font = new Font("Segoe UI", 8),
                ForeColor = colorTextoSuave,
                AutoSize = true,
                Location = new Point(400, 22)
            };

            p.Controls.Add(lblProv);
            p.Controls.Add(cmbProveedor);
            p.Controls.Add(btnCrear);
            p.Controls.Add(indicadorEstado);
            p.Controls.Add(lblEstado);
            return p;
        }

        private Panel CrearPanelConversacion()
        {
            Panel p = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = colorTarjeta,
                Padding = new Padding(10),
                Margin = new Padding(0, 3, 0, 3)
            };
            p.Paint += (s, e) => PintarBordeRedondeado(p, e.Graphics);

            rtbConversacion = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new Font("Segoe UI", 10),
                BackColor = colorTarjeta,
                BorderStyle = BorderStyle.None
            };

            rtbConversacion.SelectionColor = colorTextoSuave;
            rtbConversacion.SelectionFont = new Font("Segoe UI", 9, FontStyle.Italic);
            rtbConversacion.AppendText("¡Hola! Soy tu planificador de estudio personal. Presiona 'Crear Agente' para comenzar.\n\n");

            p.Controls.Add(rtbConversacion);
            return p;
        }

        private Panel CrearPanelInput()
        {
            Panel p = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = colorTarjeta,
                Padding = new Padding(10),
                Margin = new Padding(0, 3, 0, 0)
            };
            p.Paint += (s, e) => PintarBordeRedondeado(p, e.Graphics);

            TableLayoutPanel layoutInput = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            layoutInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutInput.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));

            txtConsulta = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = colorFondo,
                Margin = new Padding(0, 3, 5, 3)
            };
            txtConsulta.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; BtnEnviar_Click(s, e); }
            };

            btnEnviar = new Button
            {
                Text = "➤ Enviar",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold),
                BackColor = colorAcento,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Margin = new Padding(5, 3, 0, 3)
            };
            btnEnviar.FlatAppearance.BorderSize = 0;
            btnEnviar.Click += BtnEnviar_Click;
            btnEnviar.Paint += (s, e) => PintarBotonRedondeado(btnEnviar, e.Graphics);

            layoutInput.Controls.Add(txtConsulta, 0, 0);
            layoutInput.Controls.Add(btnEnviar, 1, 0);
            p.Controls.Add(layoutInput);
            return p;
        }

        private void BtnCrear_Click(object? sender, EventArgs e)
        {
            try
            {
                _agente = FabricaAgente.CrearAgente(cmbProveedor.Text, "Planificador",
                    "ayuda al estudiante a organizar su tiempo.");
                lblEstado.Text = "conectado";
                indicadorEstado.Invalidate();
                AgregarMensaje("sistema", "agente creado correctamente. ¡listo para planificar!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al crear agente: " + ex.Message);
            }
        }

        private async void BtnEnviar_Click(object? sender, EventArgs e)
        {
            if (_agente == null)
            {
                MessageBox.Show("primero debes crear el agente.");
                return;
            }

            string consulta = txtConsulta.Text.Trim();
            if (string.IsNullOrEmpty(consulta)) return;

            AgregarMensaje("tú", consulta);
            txtConsulta.Clear();

            try
            {
                var respuesta = await _agente.RunAsync(consulta);
                AgregarMensaje("agente", respuesta.Text);
                // notificamos al dashboard que hay una nueva respuesta
                RespuestaRecibida?.Invoke(respuesta.Text);
            }
            catch (Exception ex)
            {
                AgregarMensaje("error", ex.Message);
            }
        }

        private void AgregarMensaje(string remitente, string mensaje)
        {
            rtbConversacion.SelectionStart = rtbConversacion.TextLength;
            rtbConversacion.SelectionLength = 0;

            if (remitente == "tú")
            {
                rtbConversacion.SelectionAlignment = HorizontalAlignment.Right;
                rtbConversacion.SelectionColor = colorAcento;
                rtbConversacion.SelectionFont = new Font("Segoe UI Semibold", 9, FontStyle.Bold);
                rtbConversacion.AppendText("tú:\n");

                rtbConversacion.SelectionAlignment = HorizontalAlignment.Right;
                rtbConversacion.SelectionColor = colorTexto;
                rtbConversacion.SelectionFont = new Font("Segoe UI", 10);
                rtbConversacion.AppendText($"{mensaje}\n\n");
            }
            else if (remitente == "agente")
            {
                rtbConversacion.SelectionAlignment = HorizontalAlignment.Left;
                rtbConversacion.SelectionColor = Color.FromArgb(72, 187, 120);
                rtbConversacion.SelectionFont = new Font("Segoe UI Semibold", 9, FontStyle.Bold);
                rtbConversacion.AppendText("🤖 agente:\n");

                rtbConversacion.SelectionAlignment = HorizontalAlignment.Left;
                rtbConversacion.SelectionColor = colorTexto;
                rtbConversacion.SelectionFont = new Font("Segoe UI", 10);
                rtbConversacion.AppendText($"{mensaje}\n\n");
            }
            else if (remitente == "sistema")
            {
                rtbConversacion.SelectionAlignment = HorizontalAlignment.Center;
                rtbConversacion.SelectionColor = colorTextoSuave;
                rtbConversacion.SelectionFont = new Font("Segoe UI", 8, FontStyle.Italic);
                rtbConversacion.AppendText($"[ {mensaje} ]\n\n");
            }
            else
            {
                rtbConversacion.SelectionAlignment = HorizontalAlignment.Center;
                rtbConversacion.SelectionColor = Color.FromArgb(245, 101, 101);
                rtbConversacion.SelectionFont = new Font("Segoe UI", 8, FontStyle.Italic);
                rtbConversacion.AppendText($"[ error: {mensaje} ]\n\n");
            }

            rtbConversacion.ScrollToCaret();
        }

        // ============ HELPERS ============
        private void PintarBordeRedondeado(Panel p, Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = p.ClientRectangle;
            rect.Width -= 1; rect.Height -= 1;
            using (var path = CrearRectanguloRedondeado(rect, 8))
            using (var lapiz = new Pen(colorBorde, 1))
                g.DrawPath(lapiz, path);
        }

        private void PintarBotonRedondeado(Button btn, Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = btn.ClientRectangle;
            rect.Width -= 1; rect.Height -= 1;
            using (var path = CrearRectanguloRedondeado(rect, 6))
            using (var brush = new SolidBrush(btn.BackColor))
                g.FillPath(brush, path);
            TextRenderer.DrawText(g, btn.Text, btn.Font, btn.ClientRectangle, btn.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private GraphicsPath CrearRectanguloRedondeado(Rectangle rect, int radio)
        {
            var path = new GraphicsPath();
            int d = radio * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace AgenteProblemaWinForms
{
    public class VistaDashboard : UserControl
    {
        private readonly Color colorFondo = Color.FromArgb(245, 247, 250);
        private readonly Color colorTarjeta = Color.White;
        private readonly Color colorTexto = Color.FromArgb(45, 55, 72);
        private readonly Color colorTextoSuave = Color.FromArgb(113, 128, 150);
        private readonly Color colorBorde = Color.FromArgb(226, 232, 240);
        private readonly Color colorAcento = Color.FromArgb(74, 144, 164);

        // contenedores que se van a rellenar dinamicamente
        private FlowLayoutPanel listaMaterias = null!;
        private TableLayoutPanel tablaHorario = null!;
        private Panel panelDistribucion = null!;
        private Panel panelHitos = null!;
        private Panel panelConsejo = null!;
        private RichTextBox rtbRespuesta = null!;
        private Button btnDescargarTXT = null!;

        // guardamos el plan actual para exportarlo
        private PlanEstudio? _planActual;

        public VistaDashboard()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = colorFondo;
            this.Padding = new Padding(10);
            InicializarComponentes();
        }

        // ============ METODO PUBLICO PARA RECIBIR LA RESPUESTA DE LA IA ============
        public void MostrarRespuestaAgente(string texto)
        {
            // limpiamos posibles bloques markdown que la ia ponga por error
            string limpio = LimpiarJSON(texto);

            try
            {
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var plan = JsonSerializer.Deserialize<PlanEstudio>(limpio, opciones);

                if (plan != null && (plan.Materias != null || plan.Horario != null))
                {
                    _planActual = plan;
                    RefrescarMaterias(plan.Materias);
                    RefrescarHorario(plan.Horario);
                    RefrescarDistribucion(plan.Distribucion);
                    RefrescarHitos(plan.Hitos);
                    RefrescarConsejo(plan.Consejo);
                    rtbRespuesta.Text = plan.Resumen ?? "plan generado correctamente.";
                }
                else
                {
                    _planActual = null;
                    rtbRespuesta.Text = texto;
                }
            }
            catch
            {
                _planActual = null;
                rtbRespuesta.Text = texto;
            }

            rtbRespuesta.SelectionStart = 0;
            rtbRespuesta.ScrollToCaret();
        }

        // limpia bloques markdown ```json ... ``` que a veces la ia añade
        private string LimpiarJSON(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return texto;

            string t = texto.Trim();

            if (t.StartsWith("```json")) t = t.Substring(7).Trim();
            else if (t.StartsWith("```")) t = t.Substring(3).Trim();

            if (t.EndsWith("```")) t = t.Substring(0, t.Length - 3).Trim();

            int inicio = t.IndexOf('{');
            int fin = t.LastIndexOf('}');
            if (inicio >= 0 && fin > inicio)
                t = t.Substring(inicio, fin - inicio + 1);

            return t;
        }

        private void InicializarComponentes()
        {
            TableLayoutPanel tabla = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = colorFondo
            };
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27F));
            this.Controls.Add(tabla);

            tabla.Controls.Add(CrearPanelMaterias(), 0, 0);
            tabla.Controls.Add(CrearPanelHorario(), 1, 0);
            tabla.Controls.Add(CrearPanelEstadisticas(), 2, 0);
        }

        // ============ COLUMNA 1: MATERIAS ============
        private Panel CrearPanelMaterias()
        {
            Panel tarjeta = CrearTarjeta();
            tarjeta.Margin = new Padding(0, 0, 5, 0);

            Label lblTitulo = new Label
            {
                Text = "📚  Mis Materias",
                Font = new Font("Segoe UI Semibold", 11, FontStyle.Bold),
                ForeColor = colorTexto,
                Dock = DockStyle.Top,
                Height = 30
            };

            listaMaterias = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };

            listaMaterias.SizeChanged += (s, e) =>
            {
                foreach (Control c in listaMaterias.Controls)
                    c.Width = listaMaterias.ClientSize.Width - 25;
            };

            tarjeta.Controls.Add(listaMaterias);
            tarjeta.Controls.Add(lblTitulo);
            return tarjeta;
        }

        private void RefrescarMaterias(List<MateriaPlan>? materias)
        {
            listaMaterias.Controls.Clear();
            if (materias == null) return;

            foreach (var m in materias)
            {
                Color color = ParsearColor(m.Color, Color.FromArgb(129, 199, 212));
                listaMaterias.Controls.Add(CrearTarjetaMateria(m.Nombre ?? "Materia", color, m.Dificultad ?? ""));
            }
        }

        private Panel CrearTarjetaMateria(string nombre, Color color, string dificultad)
        {
            Panel t = new Panel
            {
                Height = 55,
                Width = 180,
                BackColor = colorFondo,
                Margin = new Padding(0, 0, 0, 6)
            };

            t.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = t.ClientRectangle;
                rect.Width -= 1; rect.Height -= 1;
                using (var path = CrearRectanguloRedondeado(rect, 8))
                using (var brush = new SolidBrush(Color.White))
                using (var lapiz = new Pen(color, 2))
                {
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(lapiz, path);
                }
            };

            Panel indicador = new Panel { Size = new Size(10, 10), Location = new Point(12, 22) };
            indicador.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(new SolidBrush(color), 0, 0, 10, 10);
            };

            Label lblNombre = new Label
            {
                Text = nombre,
                Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold),
                ForeColor = colorTexto,
                Location = new Point(30, 10),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            Label lblDif = new Label
            {
                Text = dificultad,
                Font = new Font("Segoe UI", 8),
                ForeColor = colorTextoSuave,
                Location = new Point(30, 30),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            t.Controls.Add(indicador);
            t.Controls.Add(lblNombre);
            t.Controls.Add(lblDif);
            return t;
        }

        // ============ COLUMNA 2: HORARIO ============
        private Panel CrearPanelHorario()
        {
            Panel tarjeta = CrearTarjeta();
            tarjeta.Margin = new Padding(3, 0, 3, 0);

            Label lblTitulo = new Label
            {
                Text = "📅  Semana Actual",
                Font = new Font("Segoe UI Semibold", 11, FontStyle.Bold),
                ForeColor = colorTexto,
                Dock = DockStyle.Top,
                Height = 30
            };

            tablaHorario = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 6,
                RowCount = 6
            };

            for (int i = 0; i < 6; i++)
                tablaHorario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6F));
            for (int i = 0; i < 6; i++)
                tablaHorario.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6F));

            string[] dias = { "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb" };
            for (int i = 0; i < 6; i++)
            {
                tablaHorario.Controls.Add(new Label
                {
                    Text = dias[i],
                    Font = new Font("Segoe UI Semibold", 8, FontStyle.Bold),
                    ForeColor = colorTextoSuave,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                }, i, 0);
            }

            for (int r = 1; r < 6; r++)
            {
                for (int c = 0; c < 6; c++)
                {
                    Panel celda = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = Color.FromArgb(250, 251, 252),
                        Margin = new Padding(1),
                        Name = $"celda_{r}_{c}"
                    };
                    tablaHorario.Controls.Add(celda, c, r);
                }
            }

            tarjeta.Controls.Add(tablaHorario);
            tarjeta.Controls.Add(lblTitulo);
            return tarjeta;
        }

        private void RefrescarHorario(List<BloqueHorario>? horario)
        {
            for (int r = 1; r < 6; r++)
            {
                for (int c = 0; c < 6; c++)
                {
                    var celda = tablaHorario.GetControlFromPosition(c, r) as Panel;
                    if (celda != null)
                    {
                        celda.Controls.Clear();
                        celda.BackColor = Color.FromArgb(250, 251, 252);
                    }
                }
            }

            if (horario == null) return;

            foreach (var b in horario)
            {
                if (b.Dia < 0 || b.Dia > 5 || b.Hora < 1 || b.Hora > 5) continue;

                var celda = tablaHorario.GetControlFromPosition(b.Dia, b.Hora) as Panel;
                if (celda == null) continue;

                Color color = ParsearColor(b.Color, Color.FromArgb(146, 180, 215));
                celda.BackColor = color;
                celda.Controls.Add(new Label
                {
                    Text = b.Materia ?? "",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 7, FontStyle.Bold),
                    ForeColor = colorTexto
                });
            }
        }

        // ============ COLUMNA 3: ESTADISTICAS ============
        private Panel CrearPanelEstadisticas()
        {
            TableLayoutPanel cont = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = colorFondo,
                Margin = new Padding(5, 0, 0, 0)
            };
            cont.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            cont.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            cont.RowStyles.Add(new RowStyle(SizeType.Percent, 12F));
            cont.RowStyles.Add(new RowStyle(SizeType.Percent, 53F));

            cont.Controls.Add(CrearPanelDistribucion(), 0, 0);
            cont.Controls.Add(CrearPanelHitos(), 0, 1);
            cont.Controls.Add(CrearPanelConsejo(), 0, 2);
            cont.Controls.Add(CrearPanelRespuestaAgente(), 0, 3);

            return cont;
        }

        private Panel CrearPanelDistribucion()
        {
            Panel tarjeta = CrearTarjeta();
            tarjeta.Margin = new Padding(0, 3, 0, 3);

            Label lblTitulo = new Label
            {
                Text = "📊  Distribución",
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold),
                ForeColor = colorTexto,
                Dock = DockStyle.Top,
                Height = 22
            };

            panelDistribucion = new Panel { Dock = DockStyle.Fill };

            tarjeta.Controls.Add(panelDistribucion);
            tarjeta.Controls.Add(lblTitulo);
            return tarjeta;
        }

        private void RefrescarDistribucion(DistribucionPlan? dist)
        {
            panelDistribucion.Controls.Clear();
            if (dist == null) return;

            Label lblEstudio = new Label { Text = $"Estudio {dist.Estudio}%", Dock = DockStyle.Top, Height = 20, Font = new Font("Segoe UI", 8), ForeColor = colorTexto };
            Label lblDescanso = new Label { Text = $"Descanso {dist.Descanso}%", Dock = DockStyle.Top, Height = 20, Font = new Font("Segoe UI", 8), ForeColor = colorTexto };
            Label lblRepaso = new Label { Text = $"Repaso {dist.Repaso}%", Dock = DockStyle.Top, Height = 20, Font = new Font("Segoe UI", 8), ForeColor = colorTexto };

            panelDistribucion.Controls.Add(lblRepaso);
            panelDistribucion.Controls.Add(lblDescanso);
            panelDistribucion.Controls.Add(lblEstudio);
        }

        private Panel CrearPanelHitos()
        {
            Panel tarjeta = CrearTarjeta();
            tarjeta.Margin = new Padding(0, 3, 0, 3);

            Label lblTitulo = new Label
            {
                Text = "🎯  Próximos Hitos",
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold),
                ForeColor = colorTexto,
                Dock = DockStyle.Top,
                Height = 22
            };

            panelHitos = new Panel { Dock = DockStyle.Fill };

            tarjeta.Controls.Add(panelHitos);
            tarjeta.Controls.Add(lblTitulo);
            return tarjeta;
        }

        private void RefrescarHitos(List<string>? hitos)
        {
            panelHitos.Controls.Clear();
            if (hitos == null) return;

            foreach (var h in hitos)
            {
                Label lbl = new Label
                {
                    Text = "•  " + h,
                    Dock = DockStyle.Top,
                    Height = 20,
                    Font = new Font("Segoe UI", 8),
                    ForeColor = colorTexto
                };
                panelHitos.Controls.Add(lbl);
                lbl.BringToFront();
            }
        }

        private Panel CrearPanelConsejo()
        {
            Panel tarjeta = CrearTarjeta();
            tarjeta.Margin = new Padding(0, 3, 0, 3);

            Label lblTitulo = new Label
            {
                Text = "💡  Consejo del Día",
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold),
                ForeColor = colorTexto,
                Dock = DockStyle.Top,
                Height = 22
            };

            panelConsejo = new Panel { Dock = DockStyle.Fill };

            tarjeta.Controls.Add(panelConsejo);
            tarjeta.Controls.Add(lblTitulo);
            return tarjeta;
        }

        private void RefrescarConsejo(string? consejo)
        {
            panelConsejo.Controls.Clear();
            if (string.IsNullOrWhiteSpace(consejo)) return;

            panelConsejo.Controls.Add(new Label
            {
                Text = consejo,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = colorTextoSuave
            });
        }

        // ============ PANEL DE RESPUESTA DEL AGENTE + BOTON TXT ============
        private Panel CrearPanelRespuestaAgente()
        {
            Panel tarjeta = CrearTarjeta();
            tarjeta.Margin = new Padding(0, 3, 0, 0);

            Label lblTitulo = new Label
            {
                Text = "🤖  Respuesta del Agente",
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold),
                ForeColor = colorTexto,
                Dock = DockStyle.Top,
                Height = 22
            };

            btnDescargarTXT = new Button
            {
                Text = "⬇  Descargar TXT",
                Dock = DockStyle.Bottom,
                Height = 32,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold),
                BackColor = colorAcento,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnDescargarTXT.FlatAppearance.BorderSize = 0;
            btnDescargarTXT.Click += BtnDescargarTXT_Click;
            btnDescargarTXT.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = btnDescargarTXT.ClientRectangle;
                rect.Width -= 1; rect.Height -= 1;
                using (var path = CrearRectanguloRedondeado(rect, 6))
                using (var brush = new SolidBrush(btnDescargarTXT.BackColor))
                    e.Graphics.FillPath(brush, path);
                TextRenderer.DrawText(e.Graphics, btnDescargarTXT.Text, btnDescargarTXT.Font,
                    btnDescargarTXT.ClientRectangle, btnDescargarTXT.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            rtbRespuesta = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new Font("Segoe UI", 9),
                BackColor = colorFondo,
                BorderStyle = BorderStyle.None,
                ForeColor = colorTexto,
                Text = "(esperando respuesta del agente...)"
            };

            tarjeta.Controls.Add(rtbRespuesta);
            tarjeta.Controls.Add(btnDescargarTXT);
            tarjeta.Controls.Add(lblTitulo);
            return tarjeta;
        }

        // ============ DESCARGA DEL TXT ============
        private void BtnDescargarTXT_Click(object? sender, EventArgs e)
        {
            bool hayContenido = _planActual != null ||
                (!string.IsNullOrWhiteSpace(rtbRespuesta.Text) &&
                 rtbRespuesta.Text != "(esperando respuesta del agente...)");

            if (!hayContenido)
            {
                MessageBox.Show("aún no hay respuesta del agente para descargar.",
                    "Sin contenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Guardar plan de estudio como TXT",
                Filter = "Archivo de texto (*.txt)|*.txt",
                FileName = "plan_estudio_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".txt"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                GenerarTXT(sfd.FileName);

                if (File.Exists(sfd.FileName))
                {
                    MessageBox.Show("TXT guardado correctamente en:\n" + sfd.FileName,
                        "Descarga exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("el archivo no se generó. intenta de nuevo.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al generar el TXT:\n" + ex.Message + "\n\n" + ex.StackTrace,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // genera el txt en texto plano formateado
        private void GenerarTXT(string ruta)
        {
            var plan = _planActual;
            string textoResumen = rtbRespuesta.Text;

            // usamos utf8 para que las tildes y las ñ se guarden correctamente
            using var writer = new StreamWriter(ruta, false, Encoding.UTF8);

            writer.WriteLine("==================================================");
            writer.WriteLine("       PLAN DE ESTUDIO PERSONALIZADO");
            writer.WriteLine("==================================================");
            writer.WriteLine("Generado el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            writer.WriteLine();

            // seccion de materias
            if (plan?.Materias != null && plan.Materias.Count > 0)
            {
                writer.WriteLine("------ MATERIAS ------");
                foreach (var m in plan.Materias)
                {
                    writer.WriteLine($"  • {m.Nombre} — {m.Dificultad}");
                }
                writer.WriteLine();
            }

            // seccion de horario
            if (plan?.Horario != null && plan.Horario.Count > 0)
            {
                writer.WriteLine("------ HORARIO SEMANAL ------");
                string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
                foreach (var b in plan.Horario)
                {
                    string dia = (b.Dia >= 0 && b.Dia < dias.Length) ? dias[b.Dia] : "?";
                    writer.WriteLine($"  • {dia} (bloque {b.Hora}): {b.Materia}");
                }
                writer.WriteLine();
            }

            // seccion de distribucion
            if (plan?.Distribucion != null)
            {
                writer.WriteLine("------ DISTRIBUCIÓN DEL TIEMPO ------");
                writer.WriteLine($"  • Estudio:  {plan.Distribucion.Estudio}%");
                writer.WriteLine($"  • Descanso: {plan.Distribucion.Descanso}%");
                writer.WriteLine($"  • Repaso:   {plan.Distribucion.Repaso}%");
                writer.WriteLine();
            }

            // seccion de hitos
            if (plan?.Hitos != null && plan.Hitos.Count > 0)
            {
                writer.WriteLine("------ PRÓXIMOS HITOS ------");
                foreach (var h in plan.Hitos)
                    writer.WriteLine($"  • {h}");
                writer.WriteLine();
            }

            // consejo
            if (!string.IsNullOrWhiteSpace(plan?.Consejo))
            {
                writer.WriteLine("------ CONSEJO DEL DÍA ------");
                writer.WriteLine("  " + plan.Consejo);
                writer.WriteLine();
            }

            // resumen completo
            writer.WriteLine("------ PLAN COMPLETO ------");
            writer.WriteLine(textoResumen);
            writer.WriteLine();
            writer.WriteLine("==================================================");
            writer.WriteLine("       FIN DEL DOCUMENTO");
            writer.WriteLine("==================================================");
        }

        // ============ HELPERS ============
        private Color ParsearColor(string? hex, Color porDefecto)
        {
            if (string.IsNullOrWhiteSpace(hex)) return porDefecto;
            try { return ColorTranslator.FromHtml(hex); }
            catch { return porDefecto; }
        }

        private Panel CrearTarjeta()
        {
            Panel t = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = colorTarjeta,
                Padding = new Padding(10)
            };
            t.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = t.ClientRectangle;
                rect.Width -= 1; rect.Height -= 1;
                using (var path = CrearRectanguloRedondeado(rect, 10))
                using (var lapiz = new Pen(colorBorde, 1))
                    e.Graphics.DrawPath(lapiz, path);
            };
            return t;
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

    // ============ MODELOS PARA DESERIALIZAR EL JSON DEL AGENTE ============
    public class PlanEstudio
    {
        public List<MateriaPlan>? Materias { get; set; }
        public List<BloqueHorario>? Horario { get; set; }
        public DistribucionPlan? Distribucion { get; set; }
        public List<string>? Hitos { get; set; }
        public string? Consejo { get; set; }
        public string? Resumen { get; set; }
    }

    public class MateriaPlan
    {
        public string? Nombre { get; set; }
        public string? Dificultad { get; set; }
        public string? Color { get; set; }
    }

    public class BloqueHorario
    {
        public int Dia { get; set; }
        public int Hora { get; set; }
        public string? Materia { get; set; }
        public string? Color { get; set; }
    }

    public class DistribucionPlan
    {
        public int Estudio { get; set; }
        public int Descanso { get; set; }
        public int Repaso { get; set; }
    }
}
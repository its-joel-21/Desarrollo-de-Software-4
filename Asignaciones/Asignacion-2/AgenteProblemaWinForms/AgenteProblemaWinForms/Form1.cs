using System;
using System.Drawing;
using System.Windows.Forms;

namespace AgenteProblemaWinForms
{
    public partial class Form1 : Form
    {
        private VistaDashboard _vistaDashboard = null!;
        private VistaChat _vistaChat = null!;

        public Form1()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            _vistaDashboard = new VistaDashboard();
            _vistaChat = new VistaChat();

            pnlContenido.Controls.Clear();
            pnlContenido.Controls.Add(_vistaDashboard);
            pnlContenido.Controls.Add(_vistaChat);

            // ⚡ ESTA ES LA LINEA QUE FALTA Y POR ESO NO SE ACTUALIZA EL DASHBOARD:
            _vistaChat.RespuestaRecibida += (texto) =>
            {
                _vistaDashboard.MostrarRespuestaAgente(texto);
            };

            _vistaDashboard.Visible = true;
            _vistaChat.Visible = false;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            _vistaDashboard.Visible = true;
            _vistaChat.Visible = false;
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            _vistaDashboard.Visible = false;
            _vistaChat.Visible = true;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pnlContenido_Paint(object sender, PaintEventArgs e) { }
    }
}
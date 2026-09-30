using System;
using System.Windows.Forms;

namespace Programa
{
    public partial class Form1 : Form
    {
        // arreglo multidimensional con los chistes cargados
        // columnas: [0] = categoria, [1] = tipo, [2] = texto
        string[,]? matriz;

        public Form1()
        {
            InitializeComponent();
            txtContenido.ReadOnly = true;
        }

        // se ejecuta al abrir el formulario y engancha los botones por nombre
        private void Form1_Load(object? sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is Button b)
                {
                    switch (b.Name)
                    {
                        case "btnEjecutar":
                            b.Click -= btnEjecutar_Click;
                            b.Click += btnEjecutar_Click;
                            break;

                        case "btnCargar":
                            b.Click -= btnCargar_Click;
                            b.Click += btnCargar_Click;
                            break;

                        case "btnGuardar":
                            b.Click -= btnGuardar_Click;
                            b.Click += btnGuardar_Click;
                            break;

                        case "btnSave":
                            b.Click -= btnSave_Click;
                            b.Click += btnSave_Click;
                            break;

                        case "btnLimpiar":
                            b.Click -= btnLimpiar_Click;
                            b.Click += btnLimpiar_Click;
                            break;

                        case "btnDelete":
                            b.Click -= btnDelete_Click;
                            b.Click += btnDelete_Click;
                            break;
                    }
                }
            }
        }

        // ------------------ eventos del textbox (por si existen) ------------------

        private void textBox1_TextChanged(object? sender, EventArgs e)
        {
        }

        private void txtContenido_TextChanged(object? sender, EventArgs e)
        {
        }

        // ------------------ eventos del combo (por si existe) ------------------

        private void cbGuardar_SelectedIndexChanged(object? sender, EventArgs e)
        {
        }

        // ------------------ boton ejecutar (llama a la api) ------------------

        private async void btnEjecutar_Click(object? sender, EventArgs e)
        {
            var cargado = await ApiChistes.Cargar(5, "Programming");
            if (cargado != null)
            {
                matriz = cargado;
                MostrarMatriz();
            }
        }

        // alias por si el designer apunta a este nombre
        private async void btnEjecutar_Click_1(object? sender, EventArgs e)
        {
            btnEjecutar_Click(sender, e);
        }

        // ------------------ boton cargar (llama a BuscadorJson) ------------------

        private void btnCargar_Click(object? sender, EventArgs e)
        {
            var cargado = Buscador.BuscarYCargar();
            if (cargado != null)
            {
                matriz = cargado;
                MostrarMatriz();
            }
        }

        private void btnCargar_Click_1(object? sender, EventArgs e)
        {
            btnCargar_Click(sender, e);
        }

        // ------------------ boton guardar (llama a Guardador) ------------------

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            Guardador.PedirYGuardar(matriz);
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            btnGuardar_Click(sender, e);
        }

        // ------------------ boton limpiar ------------------

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtContenido.Clear();
            matriz = null;
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            btnLimpiar_Click(sender, e);
        }

        // ------------------ metodo auxiliar ------------------

        // vuelca la matriz al textbox de contenido
        private void MostrarMatriz()
        {
            if (matriz == null || txtContenido == null) return;

            txtContenido.Clear();
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                txtContenido.AppendText(
                    $"[{matriz[i, 0]}] ({matriz[i, 1]}){Environment.NewLine}" +
                    $"{matriz[i, 2]}{Environment.NewLine}{Environment.NewLine}");
            }
        }
    }

    // ------------------ modelo compartido ------------------

    // dto compartido entre Form1, ApiChistes, BuscadorJson y Guardador
    public class ChisteDto
    {
        public string? Categoria { get; set; }
        public string? Tipo { get; set; }
        public string? Texto { get; set; }
    }
}
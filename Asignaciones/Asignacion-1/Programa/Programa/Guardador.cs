using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace Programa
{
    // se encarga de preguntar el formato y guardar la matriz en txt, json o csv
    public static class Guardador
    {
        // pregunta el formato y luego pide al usuario donde guardar
        public static void PedirYGuardar(string[,]? matriz)
        {
            if (matriz == null)
            {
                MessageBox.Show("primero debes ejecutar la api o cargar un archivo");
                return;
            }

            // Sí = TXT, No = JSON, Cancel = CSV
            DialogResult r = MessageBox.Show(
                "¿En qué formato quieres guardar?\n\n" +
                "Sí  = TXT\n" +
                "No  = JSON\n" +
                "Cancelar = CSV",
                "Elegir formato",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (r == DialogResult.Yes) // Guardar como TXT
            {
                using SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Guardar como TXT";
                sfd.Filter = "Archivo de texto (*.txt)|*.txt";
                sfd.FileName = "chistes.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GuardarTxt(matriz, sfd.FileName);
                    MessageBox.Show("Guardado en: " + sfd.FileName);
                }
            }
            else if (r == DialogResult.No) // Guardar como JSON
            {
                using SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Guardar como JSON";
                sfd.Filter = "Archivo JSON (*.json)|*.json";
                sfd.FileName = "chistes.json";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GuardarJson(matriz, sfd.FileName);
                    MessageBox.Show("Guardado en: " + sfd.FileName);
                }
            }
            else if (r == DialogResult.Cancel) // Guardar como CSV
            {
                using SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Guardar como CSV";
                sfd.Filter = "Archivo CSV (*.csv)|*.csv";
                sfd.FileName = "chistes.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GuardarCSV(matriz, sfd.FileName);
                    MessageBox.Show("Guardado en: " + sfd.FileName);
                }
            }
        }

        // exporta el arreglo a un archivo de texto plano
        private static void GuardarTxt(string[,] m, string ruta)
        {
            using var writer = new StreamWriter(ruta);
            int filas = m.GetLength(0);
            int cols = m.GetLength(1);

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    writer.Write(m[i, j]);
                    if (j < cols - 1) writer.Write(" | ");
                }
                writer.WriteLine();
            }
        }

        // exporta el arreglo a un archivo json
        private static void GuardarJson(string[,] m, string ruta)
        {
            int filas = m.GetLength(0);
            var lista = new List<ChisteDto>(filas);

            for (int i = 0; i < filas; i++)
            {
                lista.Add(new ChisteDto
                {
                    Categoria = m[i, 0],
                    Tipo = m[i, 1],
                    Texto = m[i, 2]
                });
            }

            // opciones para que el json quede legible con tildes
            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(lista, opciones);
            File.WriteAllText(ruta, json);
        }

        // NUEVO: exporta el arreglo a un archivo CSV estándar
        private static void GuardarCSV(string[,] m, string ruta)
        {
            // Usamos UTF8 para que las tildes se guarden correctamente
            using var writer = new StreamWriter(ruta, false, Encoding.UTF8);
            int filas = m.GetLength(0);
            int cols = m.GetLength(1);

            // Escribir encabezados (opcional, pero recomendado para Excel)
            writer.WriteLine("Categoria,Tipo,Texto");

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    string valor = m[i, j] ?? "";

                    // Si el texto contiene comas, comillas o saltos de línea, 
                    // se debe encerrar entre comillas dobles y escapar las comillas internas.
                    if (valor.Contains(",") || valor.Contains("\"") || valor.Contains("\n") || valor.Contains("\r"))
                    {
                        valor = "\"" + valor.Replace("\"", "\"\"") + "\"";
                    }

                    writer.Write(valor);
                    if (j < cols - 1) writer.Write(",");
                }
                writer.WriteLine();
            }
        }
    }
}
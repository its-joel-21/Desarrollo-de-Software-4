using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO; // Necesario para leer CSV robusto

namespace Programa
{
    // Se encarga de buscar un archivo (JSON o CSV) en el disco y cargarlo en una matriz
    public static class Buscador
    {
        // Abre un diálogo para elegir el tipo de archivo y luego el archivo.
        // Devuelve la matriz [n, 3] o null si el usuario cancela o hay error.
        public static string[,]? BuscarYCargar()
        {
            // 1. Preguntar al usuario qué tipo de archivo desea cargar
            DialogResult eleccion = MessageBox.Show(
                "¿Qué tipo de archivo deseas cargar?\n\nSí = CSV\nNo = JSON",
                "Seleccionar formato",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (eleccion == DialogResult.Cancel) return null;

            using OpenFileDialog ofd = new OpenFileDialog();

            // 2. Configurar el filtro y título según la elección
            if (eleccion == DialogResult.Yes) // CSV
            {
                ofd.Title = "Selecciona un archivo CSV";
                ofd.Filter = "Archivo CSV (*.csv)|*.csv|Todos los archivos (*.*)|*.*";
            }
            else // JSON
            {
                ofd.Title = "Selecciona un archivo JSON";
                ofd.Filter = "Archivo JSON (*.json)|*.json|Todos los archivos (*.*)|*.*";
            }

            if (ofd.ShowDialog() != DialogResult.OK) return null;

            // 3. Procesar según la elección
            try
            {
                return eleccion == DialogResult.Yes
                    ? CargarCSV(ofd.FileName)
                    : CargarJSON(ofd.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el archivo: " + ex.Message);
                return null;
            }
        }

        // === Método original para JSON ===
        private static string[,]? CargarJSON(string ruta)
        {
            string contenido = File.ReadAllText(ruta);

            // Se espera una lista de objetos con categoria, tipo y texto
            var lista = JsonSerializer.Deserialize<List<ChisteDto>>(contenido);

            if (lista == null || lista.Count == 0)
            {
                MessageBox.Show("El archivo JSON está vacío o no tiene el formato esperado");
                return null;
            }

            var matriz = new string[lista.Count, 3];
            for (int i = 0; i < lista.Count; i++)
            {
                matriz[i, 0] = lista[i].Categoria ?? "";
                matriz[i, 1] = lista[i].Tipo ?? "";
                matriz[i, 2] = lista[i].Texto ?? "";
            }

            MessageBox.Show("JSON cargado correctamente");
            return matriz;
        }

        // === Nuevo método para CSV ===
        private static string[,]? CargarCSV(string ruta)
        {
            var filas = new List<string[]>();

            // TextFieldParser maneja correctamente comas dentro de comillas dobles
            using (var parser = new TextFieldParser(ruta))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                while (!parser.EndOfData)
                {
                    string[]? campos = parser.ReadFields();
                    if (campos != null) filas.Add(campos);
                }
            }

            if (filas.Count == 0)
            {
                MessageBox.Show("El archivo CSV está vacío");
                return null;
            }

            // Detectar si la primera fila es encabezado (no contiene datos reales)
            int inicioDatos = 0;
            string primerCampo = filas[0][0].Trim().ToLower();
            if (primerCampo == "categoria" || primerCampo == "categoría")
            {
                inicioDatos = 1; // Saltamos el encabezado
            }

            int totalFilas = filas.Count - inicioDatos;
            if (totalFilas <= 0)
            {
                MessageBox.Show("El CSV no contiene datos (solo encabezado)");
                return null;
            }

            var matriz = new string[totalFilas, 3];

            for (int i = 0; i < totalFilas; i++)
            {
                string[] campos = filas[i + inicioDatos];

                if (campos.Length < 3)
                {
                    throw new Exception($"La fila {i + inicioDatos + 1} del CSV no tiene las 3 columnas requeridas (Categoría, Tipo, Texto).");
                }

                matriz[i, 0] = campos[0]?.Trim() ?? "";
                matriz[i, 1] = campos[1]?.Trim() ?? "";
                matriz[i, 2] = campos[2]?.Trim() ?? "";
            }

            MessageBox.Show($"CSV cargado correctamente ({totalFilas} registros)");
            return matriz;
        }
    }
}
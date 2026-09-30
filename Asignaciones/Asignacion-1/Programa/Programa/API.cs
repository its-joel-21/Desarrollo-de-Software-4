using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programa
{
    // se encarga de consultar la jokeapi y devolver la matriz de chistes
    public static class ApiChistes
    {
        // consulta la api y devuelve una matriz [n, 3]
        // columnas: [0] = categoria, [1] = tipo, [2] = texto
        // si falla, devuelve null
        public static async Task<string[,]?> Cargar(int cantidad, string categoria)
        {
            // si la categoría viene vacía o nula, se usa "Any" por defecto
            string categoriaUrl = string.IsNullOrWhiteSpace(categoria) ? "Any" : categoria;

            // el orden correcto de los parámetros es: ?lang=es&amount=5&safe-mode
            string url = $"https://v2.jokeapi.dev/joke/{categoriaUrl}?lang=es&amount={cantidad}&safe-mode";

            using var http = new HttpClient();

            try
            {
                var llamado = await http.GetFromJsonAsync<JokeListResponse>(url);

                if (llamado == null || llamado.Jokes == null || llamado.Jokes.Count == 0)
                {
                    MessageBox.Show("no se pudieron obtener chistes");
                    return null;
                }

                int n = llamado.Jokes.Count;
                var matriz = new string[n, 3];

                for (int i = 0; i < n; i++)
                {
                    var j = llamado.Jokes[i];
                    matriz[i, 0] = j.Category ?? "";
                    matriz[i, 1] = j.Type ?? "";
                    matriz[i, 2] = j.Type == "single"
                        ? (j.Joke ?? "")
                        : $"{j.Setup} — {j.Delivery}";
                }

                return matriz;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al consultar la api: " + ex.Message);
                return null;
            }
        }
    }

    // ------------------ modelos de la api ------------------

    // respuesta general de la api cuando se usa ?amount=
    public class JokeListResponse
    {
        [System.Text.Json.Serialization.JsonPropertyName("error")]
        public bool Error { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("amount")]
        public int Amount { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("jokes")]
        public List<JokeResponse>? Jokes { get; set; }
    }

    // un chiste individual
    public class JokeResponse
    {
        [System.Text.Json.Serialization.JsonPropertyName("category")]
        public string? Category { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("type")]
        public string? Type { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("joke")]
        public string? Joke { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("setup")]
        public string? Setup { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("delivery")]
        public string? Delivery { get; set; }
    }
}
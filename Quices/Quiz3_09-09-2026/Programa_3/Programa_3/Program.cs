// De: Álvaro Rodríguez

using System.Diagnostics;
using System.IO;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        //variables
        string nombre;
        int CantidadDeBoletos, categoraía;
        decimal tarifa, SubTotal, recargo = 0, total;

        string carpeta = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\"));//obtiene la ruta
        string ruta = Path.Combine(carpeta, "Formatiovo1_P3.txt");//guarda la ruta

        Console.Write($"Ingrese el nombre del usuario: "); //piude nombre
        nombre = Console.ReadLine();

        Console.Write($"\nIngrese la cantidad de boletos: "); // pide cantidad d boletos
        CantidadDeBoletos = int.Parse(Console.ReadLine());

        Console.Write($"\nIngrese el tipo de catgoría:\n1. General\n2. Preferecnial\n3. VIP\nSeleccione una: "); //´pide tipo de categoría
        categoraía = int.Parse(Console.ReadLine());

        if (CantidadDeBoletos >= 0 && CantidadDeBoletos <= 10) // vaida qu el rango de boletos est entre 0 a 10
        {
            tarifa = categoraía switch // obtiene la tarifa en función de la categoría
            {
                1 => 15.00m,
                2 => 30.00m,
                3 => 50.00m,
                _ => 0
            };
            //calculos finales
            SubTotal = CantidadDeBoletos + tarifa;
            if (CantidadDeBoletos >= 5) recargo = SubTotal * 0.12m;
            total = recargo + SubTotal;
            //variable con la información d eimpresión
            string impresion = "==============================\n" +
                               "========= RESULTADOS =========\n" +
                               $"Creado: {DateTime.Now:dd / mm / yyyy HH: mm: ss}" +
                               "\nNombre: " + nombre + "\n" +
                               "Consumo kwh: " + CantidadDeBoletos + "\n" +
                               "Subotal: " + SubTotal + "\n" +
                               "Total: " + total;

            try// previene errores
            {
                File.WriteAllText(ruta, impresion);
                Console.WriteLine("Archivo creado correctamente");
                Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });
            }
            catch (IOException ex) { Console.WriteLine($"Error de E/S: {ex.Message}"); }

        }
        else
        {
            Console.WriteLine("La cantifdad de kilovatios consumidos es menor a cero"); //en caso de cantidas de boletos fuera del rango
        }

    }
}
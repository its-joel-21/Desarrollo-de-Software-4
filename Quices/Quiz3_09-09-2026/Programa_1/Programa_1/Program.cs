// De: Álvaro Rodríguez

using System.Text;
using System.IO;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        //variables
        string NombreDeCliente;
        int Kwh, TipoDeCliente;
        decimal tarifa, SubTotal, recargo, total;

        string carpeta = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\")); //obtiene la uta para guardar
        string ruta = Path.Combine(carpeta, "Formatiovo1_P1.txt");//tiene la ruta

        Console.Write($"Ingrese el nombre del usuario: "); //pide nombre
        NombreDeCliente = Console.ReadLine();

        Console.Write($"\nIngrese la cantidad de kilovatios consumidos: ");//pide kilovatios consumidos
        Kwh = int.Parse(Console.ReadLine());

        Console.Write($"\nIngrese el tipo de cliente:\n1. Residencial\n2. Comercial\n3. Industrial\nSeleccione una: "); // pide tipo de cliente
        TipoDeCliente = int.Parse(Console.ReadLine());

        if (Kwh >= 0) // si kwh es mayor o igual a cero
        {
            tarifa = TipoDeCliente switch //obtiene la taria segun el tipo de cliente
            {
                1 => 0.15m,
                2 => 0.22m,
                3 => 0.30m,
                _ => 0
            };

            //calcula los totales finales
            SubTotal = Kwh + tarifa;
            recargo = SubTotal * 0.07m;
            total = recargo + SubTotal;
            // variable que guarda la información para imprimir
            string impresion = "==============================\n" +
                               "========= RESULTADOS =========\n" +
                               $"Creado: {DateTime.Now:dd / mm / yyyy HH: mm: ss}" +
                               "\nNombre: " + NombreDeCliente + "\n" +
                               "Consumo kwh: " + Kwh + "\n" +
                               "Subotal: " + SubTotal + "\n" +
                               "Total: " + total;

            try //previene error
            {
                File.WriteAllText(ruta, impresion); //guarda archivo
                Console.WriteLine("Archivo creado correctamente");
                Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true }); // abre el archivo
            }
            catch (IOException ex) { Console.WriteLine($"Error de E/S: {ex.Message}"); }

        }
        else
        {
            Console.WriteLine("La cantifdad de kilovatios consumidos es menor a cero"); // si los kilovatios son menores 
        }

    }
}
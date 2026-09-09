// De: Álvaro Rodríguez

using System.Diagnostics;
using System.IO;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        //variables
        string placa;
        int HorasEstacionado, TipoDeVehiculo;
        decimal tarifa, SubTotal, recargo = 0, total;

        string carpeta = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\")); //obtiene la ruta
        string ruta = Path.Combine(carpeta, "Formatiovo1_P2.txt");// guarda la ruta

        Console.Write($"Ingrese la placa del vehiculo: "); //pide la placa
        placa = Console.ReadLine();

        Console.Write($"Ingrese las horas que ha estado estacionado el vehículo: "); //pide las horas estacionado
        HorasEstacionado = int.Parse(Console.ReadLine());

        Console.Write($"Ingrese el tip de vehiculo:\n1. Moto\n2. Auto\n3. Camioneta\nSeleccione: "); //pide el tipo de vehiculo
        TipoDeVehiculo = int.Parse(Console.ReadLine());

        if (HorasEstacionado >= 1 && HorasEstacionado <= 24) //valida que las horas estacionada este dentroi del rango de 1 a 24
        {

            tarifa = TipoDeVehiculo switch // se obtiene la taria en función del tipo de vehículo
            {
                1 => 0.75m,
                2 => 1.50m,
                3 => 2.25m,
                _ => 0
            };
            //calculos finales
            SubTotal = HorasEstacionado + tarifa;
            if (HorasEstacionado > 8) recargo = SubTotal * 0.10m;
            total = recargo + SubTotal;
            // variable para la impresión
            string impresion = "==============================\n" +
                               "========= RESULTADOS =========\n" +
                               $"Creado: {DateTime.Now:dd / mm / yyyy HH: mm: ss}" +
                               "\nPlaca: " + placa + "\n" +
                               "Horas estacionado: " + HorasEstacionado + "\n" +
                               "Subotal: " + SubTotal + "\n" +
                               "Total: " + total;

            try//previene errores
            {
                File.WriteAllText(ruta, impresion); //guarda en la ruta
                Console.WriteLine("Archivo creado correctamente");
                Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true }); // abre el archivo
            }
            catch (IOException ex) { Console.WriteLine($"Error de E/S: {ex.Message}"); }

        }
        else
        {  // en caso de que tiempo este fuera del rango
            Console.WriteLine($"El tiempo que ha estado el auto es incorrecto");
        }

    }
}
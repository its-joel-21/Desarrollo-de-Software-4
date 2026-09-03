//De: Álvaro Rodríguez.    Fecha: 2.9.2026

/*
3. Entrada a un evento: Un sistema necesita verificar si una persona cumple las condiciones necesarias para ingresar a un 
evento. Solicite la edad, la cantidad de boletos disponibles y el tipo de entrada (1 = General, 2 = VIP, 3 = Invitado). 
Si la persona tiene 18 años o más y posee al menos un boleto y seleccionó como tipo de entrada la opción 1, 2 o 3, 
muestre “Entrada autorizada” junto con el tipo seleccionado. De lo contrario, muestre “Entrada rechazada” indicando 
que no cumple con las condiciones establecidas.
 */

using System.Text;

internal class Program
{
    private static void Main()
    {
        //variables
        int edad, CantidadDeBoletos, TipoDeEntrada;
        String mensaje = "";

        //se pide la edad
        Console.Write($"Ingrese su edad: ");
        edad = int.Parse(Console.ReadLine());

        //se pide la cantidad de boletos
        Console.Write($"Ingrese la cantidad de boletos disponibles: ");
        CantidadDeBoletos = int.Parse(Console.ReadLine());

        //Se pide el tipo de entrada
        Console.WriteLine($"Seleccione el tipo de entrada:\n1. General\n2. VIP\n3. Invitado");
        Console.Write("Seleccione: ");
        TipoDeEntrada = int.Parse(Console.ReadLine());

        //indica el tipo de usuario al imprimir
        if (TipoDeEntrada == 1) mensaje = "General";
        if (TipoDeEntrada == 2) mensaje = "VIP";
        if (TipoDeEntrada == 3) mensaje = "Invitado";

        Console.WriteLine();//salto de línea

        // if
        if (edad >= 18)//validamos si cuenta con la suficiente edad, si la cumple se sigue a
        {
            if (CantidadDeBoletos >= 1) // validar la cantidad de boletos, si tiene solo uno se pasa a ver el tipo de usuario
            {
                if (TipoDeEntrada == 1 | TipoDeEntrada == 2 | TipoDeEntrada == 3)
                {
                    Console.WriteLine("Entrada autorizada\nTipo de boleto: " + TipoDeEntrada + " [ " + mensaje + " ]");
                }
                else // si no coincide con los tipo de entrada se impide la entrada
                {
                    Console.WriteLine($"Entrada rechazada\nNo existe dicha opción");
                }
            }
            else// si no cumple con la cantidad de boletos se impide la entrada 
            {
                Console.WriteLine($"Entrada rechazada\nNo cuneta con los boletos suficientes");
            }
        }
        else// si no se cumpe con la edad se impide la entrada
        {
            Console.WriteLine($"Entrada rechazada\nNo cuenta con la edad para ingresar");
        }
    }
}
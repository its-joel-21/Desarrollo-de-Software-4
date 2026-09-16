/*
De: Álvaro Rodríguez - Fecha: 16/9/2026
Problema 1: Identificador Simple del Día de la Semana
Desarrollar un programa en C# que reciba un número entero y devuelva el nombre del día de la semana 
correspondiente. El sistema recibirá como 1 valor de entrada: el número identificador del día (int del 1 al 7). 
Realizará 1 operación simple: calcular el número del día siguiente (numero + 1). Utilizará 1 estructura switch 
con 7 opciones para asociar cada número con su respectivo día de la semana. Finalmente, arrojará 1 valor de 
salida: el nombre del día de la semana.
 */

internal class Program
{
    private static void Main(string[] args)
    {
        // variables
        int NumeroDia, i=0;
        string dia = "", DiaSiguiente = "";

        Console.Write($"Ingrese un  numero emntre 1 y 7: "); //pide numero para detectar día
        NumeroDia = int.Parse(Console.ReadLine());

    DecideDia:
        dia = NumeroDia switch // sengun el NumeroDia que se ingrese para dia se le dara un dia de la semana
        {
            1 => "Lunes",
            2 => "Martes",
            3 => "Miércoles",
            4 => "Jueves",
            5 => "Viernes",
            6 => "Sábado",
            7 => "Domingo",
            _ => "ERROR: No existe tal día"
        };
        i++; // para saber que ya encontro el dia actual

        if (i < 2) Console.WriteLine($"El día actual es: " + dia); //imprime el día actual
        NumeroDia = NumeroDia == 7 ? NumeroDia - 6: NumeroDia + 1;//incrementa el valor del NumeroDia ingresado o lo decremente según sea el caso
        if (NumeroDia != 0) { DiaSiguiente = dia; } else { NumeroDia = 0; }
        if (i == 1) goto DecideDia; // valida que el valor de i sea 1 para solo repetir una vez
        Console.WriteLine($"El día siguiente es: "+ DiaSiguiente); // imprime el valor del día siguiente que se ha ingresado
    }
}
//De: Álvaro Rodríguez.    Fecha: 2.9.2026

/*
4. Desarrolle un programa que permita determinar cuál es el número mayor entre varios valores ingresados por el usuario. Solicite cuatro 
números diferentes y almacene cada uno en una variable. Utilizando únicamente sentencias if y el operador lógico and, compare los cuatro 
valores para determinar cuál es mayor que los otros tres. Al finalizar, el programa deberá mostrar el mensaje “El número mayor es:” seguido 
del valor correspondiente. No utilice funciones como max() ni estructuras repetitivas.
 */

using System.Text;

internal class Program
{
    private static void Main()
    {
        //variables
        int numero1, numero2, numero3, numero4, ValorTemporal = 0, mayor;

        //se pide los valores numericos
        Console.Write($"Ingrese un valor númerico: ");
        numero1 = int.Parse(Console.ReadLine());

        Console.Write($"Ingrese un valor númerico: ");
        numero2 = int.Parse(Console.ReadLine());

        Console.Write($"Ingrese un valor númerico: ");
        numero3 = int.Parse(Console.ReadLine());

        Console.Write($"Ingrese un valor númerico: ");
        numero4 = int.Parse(Console.ReadLine());

        Console.WriteLine();//salto de línea

        switch (numero1) // evaluamos partiendo del dato ingresado primero
        {
            case int n1 when (n1 >= numero2 && n1 >= numero3 && n1 >= numero4): //evaluamos si el número1 es mayor que los demas
                mayor = n1; // si es así se guarda el valor
                break;
            case int n2 when (numero2 >= numero3 && numero2 >= numero4): // comparamos el numero2 con el numero3 y numero4
                mayor = numero2;// si el numero2 es mayor se guarda el valor
                break;
            case int n3 when (numero3 >= numero4): // comparamos si el numero3 es mayor que el numero4
                mayor = numero3;// si es así se guarda el valor de numero3
                break;
            default: // si todo lo anterior fallo el numero4 es el valor mayor
                mayor = numero4;
                break;
        }
        //imprimimos el número mayor
        Console.WriteLine("El número mayor es: " + mayor);

    }
}
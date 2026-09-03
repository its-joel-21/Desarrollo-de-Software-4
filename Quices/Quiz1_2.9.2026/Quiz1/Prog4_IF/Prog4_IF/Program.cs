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
        int numero1, numero2, numero3, numero4, ValorTemporal = 0;

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

        // if para validar
        if (numero1 > numero2) { ValorTemporal = numero1; numero1 = numero2; numero2 = ValorTemporal; } //valida el dato 1 y 2 y cambia de posición si 1 > 2
        if (numero2 > numero3) { ValorTemporal = numero2; numero2 = numero3; numero3 = ValorTemporal; } //valida el dato 2 y 3 y cambia posición si 2 > 3
        if (numero3 > numero4) { ValorTemporal = numero3; numero3 = numero4; numero4 = ValorTemporal; } //valida el dato 3 y 4 y cambia de posición si 3 > 4

        if (numero2 > numero3) { ValorTemporal = numero2; numero2 = numero3; numero3 = ValorTemporal; } //valida el dato 2 y 3 y cambia de posición si 2 > 3
        if (numero3 > numero4) { ValorTemporal = numero3; numero3 = numero4; numero4 = ValorTemporal; } //valida el dato 4 y 3 y cambia de posición si 4 > 3
        if (numero1 > numero2) { ValorTemporal = numero1; numero1 = numero2; numero2 = ValorTemporal; } //valida el dato 1 y 2 y cambia de posición si 1 > 2
        
        Console.WriteLine(numero1+"\n"+numero2+"\n"+numero3+"\n"+numero4);//imprime de menor a mayor
        Console.WriteLine("El mayor número es: "+numero4);//indica el dato mayor
    }
}

// De: Álvaro Rodríguez         Fecha:4.9.2026

/*

17. Consumo eléctrico
Solicite los kilovatios-hora consumidos, el tipo de cliente (1 = Residencial, 2 = Comercial, 3 = Industrial) y el mes. 
Utilice switch para establecer una tarifa según el tipo de cliente. Si el consumo supera 500 kWh, aplique un recargo. 
Muestre la tarifa utilizada, el recargo y el total a pagar.
Entradas: 3. Salidas: 3.
Utilizar: if, switch.

 */

using System.Text.RegularExpressions;

internal class Program
{
    private static void Main()
    {
        // variables
        int kilovatios, TipoDeCliente, mes;
        decimal tarifa, PorcentajeDeRecargo = 0.10m, recargo = 0, TotalAPagar;
        //guardamos en la variable meses todos los meses con un respectivo valor númerico
        string meses = "1. Enero\t2. Febrero\t3. Marzo\n4. Abril\t5. Mayo\t\t6. Junio\n7. Julio\t8. Agosto\t9. Septiembre\n10. Octubre\t11. Noviembre\t12. Diciembre";

        //variables extras solo para impresión
        string[] MES = {"Enero","Febreo","Marzo","Abril","Mayo","Junio","Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre"};

        //Pedimos el valor de los kwh
    inicio0:
        Console.Write($"\nIndique los kilovatios-hora consumidos: ");
        kilovatios = int.Parse(Console.ReadLine());
        if (kilovatios < 1) { Console.WriteLine("Kilovatior-hora incorrectos"); goto inicio0; }//valida entrada positiva y sino envia a pedir dato de nuevo

        //pedimos el tipo de usario
    inicio1:
        Console.Write($"\nIndique su tipo de cliente:\n1. Residencial\n2. Comercial\n3. Industrial\nSelecciona: ");
        TipoDeCliente = int.Parse(Console.ReadLine());
        if (!(TipoDeCliente == 1 | TipoDeCliente == 2 | TipoDeCliente == 3)) { Console.WriteLine("No existe ese cliente"); goto inicio1; }//valida que solo se seleccione los clientes disponibles y sino pide dato de nuevo

        //pedimos el mes
    inicio2:
        Console.Write($"\nIndique el mes:\n" + meses + "\nSelecciona: ");
        mes = int.Parse(Console.ReadLine());
        if (mes is not (1 or 2 or 3 or 4 or 5 or 6 or 7 or 8 or 9 or 10 or 11 or 12)) { Console.WriteLine("No existe ese mes"); goto inicio2; }//valida igualmente que el mes este dentro del rango sino para pedir de nuevo

        switch (TipoDeCliente) // ejecutamos en función del tipo de cliente
        {
            case 1: // el cliente 1 que es el residencial
                tarifa = 35.99m; //tarifa para el cliente
                if (kilovatios > 500) recargo = tarifa * PorcentajeDeRecargo; // calculo de recargo de kwh es mayor a 500
                TotalAPagar = tarifa + recargo;//calculo del total a pagar
                //impresión de información
                Console.WriteLine($"\nCliente:\tResidencial\nMes:\t\t" + (MES[mes-1])+"\nkwh:\t\t"+ kilovatios + "\nTarifa:\t\t"+tarifa+"\nRecargo (10%):\t"+ recargo+"\nTotal a pagar:\t"+TotalAPagar);
                break;

            case 2:// el cliente 2 que es el comercial
                tarifa = 71.98m; //tarifa para el cliente
                if (kilovatios > 500) recargo = tarifa * PorcentajeDeRecargo; // calculo de recargo de kwh es mayor a 500
                TotalAPagar = tarifa + recargo; //calculo del total a pagar
                //impresión de información
                Console.WriteLine($"\nCliente:\tComercial\nMes:\t\t" + (MES[mes-1]) + "\nkwh:\t\t" + kilovatios + "\nTarifa:\t\t" + tarifa + "\nRecargo (10%):\t" + recargo + "\nTotal a pagar:\t" + TotalAPagar);
                break;

            case 3:// el cliente 3 que es el industrial
                tarifa = 107.97m; //tarifa para el cliente
                if (kilovatios > 500) recargo = tarifa * PorcentajeDeRecargo; // calculo de recargo de kwh es mayor a 500
                TotalAPagar = tarifa + recargo; //calculo del total a pagar
                //impresión de información
                Console.WriteLine($"\nCliente:\tIndustrial\nMes:\t\t" + (MES[mes-1]) + "\nkwh:\t\t" + kilovatios + "\nTarifa:\t\t" + tarifa + "\nRecargo (10%):\t" + recargo + "\nTotal a pagar:\t" + TotalAPagar);
                break;

            default: //En caso de no cumplirse lo anterior
                Console.WriteLine($"\nNo existe el cliente [ " + TipoDeCliente + " ]");
                break;
        }
    }
}
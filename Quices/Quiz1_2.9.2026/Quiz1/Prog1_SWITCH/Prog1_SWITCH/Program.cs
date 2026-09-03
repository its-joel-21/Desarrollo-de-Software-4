//De: Álvaro Rodríguez.    Fecha: 2.9.2026

/*
1. Descuento en una tienda: Una tienda desea lmplementar un pequeño programa para determinar si un cliente puede recibir un descuento especial. 
Solicite el monto total de la compra y el tipo de cliente (1 = Regular, 2 = Frecuente, 3 = VIP). S1 la compra es mayor de $100 y el cliente 
pertenece al tipo 2 o al tipo 3, calcule y muestre el monto final aplicando un 15 X de descuento. De lo contrario, Informe que no recibe descuento y
muestre el monto original que deberá pagar.
 */

internal class Program
{
    private static void Main()
    {
        decimal MontoTotal, descuento, CostoFinal;
        int TipoCliente;
        String mensaje = "";

        //se pide el monto total neto de la compra
        Console.Write($"Ingrese su monto total de compra: ");
        MontoTotal = decimal.Parse(Console.ReadLine());

        //se pide el tipo de usuario
        Console.WriteLine($"Seleccione el tipo de usuario:\n1. Regular\n2. Frecuente\n3. VIP");
        Console.Write("Seleccione: ");
        TipoCliente = int.Parse(Console.ReadLine());

        //indica el tipo de usuario al imprimir
        if (TipoCliente == 1) mensaje = "Regular";
        if (TipoCliente == 2) mensaje = "Frecuente";
        if (TipoCliente == 3) mensaje = "VIP";

        Console.WriteLine(); // salto de línea

        // switch
        switch (MontoTotal) // elegimos entre las dos opciones si es mayor ejecuta el primer caso sino ejecuta el default
        {
            case > 100: // si el monto es mayor a 100 se evalua el tipo de cliente

                switch (TipoCliente)
                {
                    case 1://el cliente uno no resive descuento
                        Console.WriteLine($"El tipo de cliente es " + TipoCliente + " [ " + mensaje + " ]\nCompraTotal: " + MontoTotal + "\nDescuento: No recibira desceunto\nMontoFinal: " + MontoTotal);
                        break;
                    case 2://el cliente dos si resive descuento
                        descuento = MontoTotal * 0.15m;
                        CostoFinal = MontoTotal - descuento;
                        Console.WriteLine($"El tipo de cliente es " + TipoCliente + " [ " + mensaje + " ]\nCompraTotal: " + MontoTotal + "\nDescuento: " + descuento + "\nMontoFinal: " + CostoFinal);
                        break;
                    case 3://el cliente 3 si resive el descuento
                        descuento = MontoTotal * 0.15m;
                        CostoFinal = MontoTotal - descuento;
                        Console.WriteLine($"El tipo de cliente es " + TipoCliente + " [ " + mensaje + " ]\nCompraTotal: " + MontoTotal + "\nDescuento: " + descuento + "\nMontoFinal: " + CostoFinal);
                        break;
                    default://en caso de que se ingrese un usuario que no existe
                        Console.WriteLine("No existe dicho tipo de cliente");
                        break;
                }

                break;
            default:
                Console.WriteLine($"El tipo de cliente es " + TipoCliente + " [ " + mensaje + " ]\nCompraTotal: " + MontoTotal + "\nDescuento: No recibira descuento\nMontoFinal: " + MontoTotal);
                break;
        }
    }
}
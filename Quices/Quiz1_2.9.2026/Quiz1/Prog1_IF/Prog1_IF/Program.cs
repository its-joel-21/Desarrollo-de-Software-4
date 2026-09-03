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

        // if
        if (MontoTotal > 100) // si el monto total en mayor a 100 se evalua el cliente
        {
            if (TipoCliente == 2 | TipoCliente == 3) // si el tipo de usuario es Frecuente(2) o VIP(3) se realiza el descuento del monto neto
            {
                descuento = MontoTotal * 0.15m;
                CostoFinal = MontoTotal - descuento;

                Console.WriteLine($"El tipo de cliente es " + TipoCliente + " [ " + mensaje +" ]\nCompraTotal: " + MontoTotal + "\nDescuento: " + descuento + "\nMontoFinal: " + CostoFinal);
            }
            else // si el tipo de usuario es Regular(1) no se calcula el descuento
            {
                Console.WriteLine($"El tipo de cliente es " + TipoCliente + " [ " + mensaje + " ]\nCompraTotal: " + MontoTotal + "\nDescuento: No recibira desceunto\nMontoFinal: " + MontoTotal);
            }
        }
        else // si el monto total es menor a 100 no es necesario evaluar al cliente ya que la clausula del monto no se cumple
        {
            Console.WriteLine($"El tipo de cliente es " + TipoCliente + " [ " + mensaje + " ]\nCompraTotal: " + MontoTotal + "\nDescuento: No recibira descuento\nMontoFinal: " + MontoTotal);
        }
    }
}
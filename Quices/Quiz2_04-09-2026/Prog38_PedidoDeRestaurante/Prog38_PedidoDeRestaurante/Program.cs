
// De: Álvaro Rodríguez.         Fecha:4.9.2026

/*

38. Pedido de restaurante
Solicite el plato seleccionado (1 = Hamburguesa, 2 = Pizza, 3 = Ensalada, 4 = Pollo), la cantidad solicitada y 
si desea bebida (1 = Sí, 2 = No). Utilice switch expression para determinar el precio del plato. Calcule el total 
agregando el precio de la bebida cuando corresponda. Muestre el nombre del plato, el subtotal y el total a pagar.
Entradas: 3. Salidas: 3.
Utilizar: if, switch expression.

 */

using System.Text.RegularExpressions;

internal class Program
{
    private static void Main()
    {

        //variables
        int plato, cantidad, bebida;
        decimal PrecioPlato, PrecioBebida, SubTotal, Total;

        //variable extra para impresión
        string[] platos = { "Hamburguesa", "Pizza", "Ensalada", "Pollo" };
        string[] bebidas = { "Sí", "No" };

        //pedimos el plato
    inicio0:
        Console.Write($"\nSeleccione un plato:\n1. Hamburguesa\n2. Pizza\n3. Ensalada\n4. Pollo\nSeleccione: ");
        plato = int.Parse(Console.ReadLine());
        if(plato is not (1 or 2 or 3 or 4)) { Console.WriteLine($"No se tiene ese plato"); goto inicio0; }//valida que el plato sean los disponibles, sino vuelve a pedir

        //pedimos la cantidad del plato que se selecciono
    inicio1:
        Console.Write($"\nIngrese la cantidad del plato que selecciono: ");
        cantidad = int.Parse(Console.ReadLine());
        if(cantidad < 1) { Console.WriteLine($"Cantidad incorrecta"); goto inicio1; }//valida que la cantidad sea positiva, sino vuelve a pedir

        //pedimos si se quiere bebida o no
    inicio2:
        Console.Write($"\nDesea una bebida:\n1. Sí\n2. No\nSeleccione: ");
        bebida = int.Parse(Console.ReadLine());
        if (bebida is not (1 or 2 )) { Console.WriteLine($"No se tiene esa opcipon"); goto inicio2; }//valida si se seleciona lo disponible, sino vuelve a pedir
        PrecioBebida = (bebida == 1) ?  2.99m : 0m; // si se escogio que Sí para bebida entonces obtenemos el precio de la bebida

        //Con un switch expression obtenemos el precio del producto seleccionado
        PrecioPlato = plato switch
        {
            1 => 3.99m, // para el producto 1 = Hamburguesa
            2 => 5.99m, // para el producto 2 = Pizza
            3 => 4.99m, // para el producto 3 = Ensalada
            4 => 6.85m, // para el producto 4 = Pollo
            _=> 0.00m // por defecto ante la ausencia de un producto
        };

        if (PrecioPlato >= 1) { // cuanto se tiene el precio del producto se valida su existencia
            SubTotal = PrecioPlato * cantidad; // luego se calcula el subtotal 
            Total = SubTotal + PrecioBebida; // luego se calcula el total con bebia
            //se imprime
            Console.WriteLine($"\nPlato [" + PrecioPlato + "]:\t\t" + (platos[plato - 1]) + "\nBebida [" + PrecioBebida + "]:\t\t" + (bebidas[bebida - 1]) + "\nSubtotal:\t\t" + SubTotal + "\nTotal:\t\t\t" + Total);
        }
        else // si no se encuentr el precio se indica que no se selecciono un producto
        {
            Console.WriteLine($"No se ha seleccionado un producto para comprar");
        }
    }
}
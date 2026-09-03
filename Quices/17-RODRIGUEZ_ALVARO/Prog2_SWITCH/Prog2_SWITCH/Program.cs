//De: Álvaro Rodríguez.      Fecha: 2.9.2026

/*
2. Aprobación de estudiante: Desarrolle un programa que permita determinar si un estudiante aprueba una asignatura según su 
rendimiento y asistencia. Solicite la nota final, el porcentaje de asistencia y el tipo de estudiante (1 = Regular, 
2 = Becado, 3 = Intercambio). Si la nota es mayor o igual a 71 y la asistencia es mayor o igual al 80 % y el estudiante 
seleccionó la opción 1, 2 o 3, muestre el mensaje “Aprobado” junto con la nota obtenida. De lo contrario, muestre el 
mensaje “No aprobado” y 
su nota final.
 */

using System.Text;

internal class Program
{
    private static void Main()
    {
        //variables
        float nota, PorcentajeDeAsistencia;
        int TipoDeEstudiante;
        String mensaje = "";

        //se pide la nota final del estudiante
        Console.Write($"Ingrese su nota final: ");
        nota = float.Parse(Console.ReadLine());

        //se pide el porcentaje de asistencias
        Console.Write($"Ingrese su porcentaje de asistencias: ");
        PorcentajeDeAsistencia = float.Parse(Console.ReadLine());

        //se pide el tipo de estudiante
        Console.WriteLine($"Seleccione el tipo de estudiante:\n1. Regular\n2. Becado\n3. Intercambio");
        Console.Write("Seleccione: ");
        TipoDeEstudiante = int.Parse(Console.ReadLine());

        //se toma el tipo de etudiante para mostrarlo en los resultados
        if (TipoDeEstudiante == 1) mensaje = "Regular";
        if (TipoDeEstudiante == 2) mensaje = "Becado";
        if (TipoDeEstudiante == 3) mensaje = "Intercambio";

        Console.WriteLine();
        
        //switch
        switch (TipoDeEstudiante) {
            case 1 or 2 or 3:
                switch (nota) {
                    case >= 71 and <= 100:
                        switch (PorcentajeDeAsistencia) {
                            case >= 80 and <=100:
                                Console.WriteLine($"APROBADO\nTipo de estudiante: " + TipoDeEstudiante + " [ " + mensaje + " ]\nNota obtenida: " + nota);
                                break;
                            case >= 60 and <80:
                                
                                switch (nota - 10) {
                                    case >= 71:
                                        Console.WriteLine($"APROBADO\nTipo de estudiante: " + TipoDeEstudiante + " [ " + mensaje + " ]\nNota obtenida: " + (nota - 10));
                                        break;
                                    case >= 0 and < 71:
                                        Console.WriteLine($"NO APROBADO\nTipo de estudiante: " + TipoDeEstudiante + " [ " + mensaje + " ]\nNota obtenida: " + (nota - 10));
                                        break;
                                }
                                break;
                            case >= 1 and <60:
                                Console.WriteLine($"NO APROBADO\nTipo de estudiante: " + TipoDeEstudiante + " [ " + mensaje + " ]\nNota obtenida: " + 0);
                                break;
                            default:
                                Console.WriteLine("Error en el dato de porcentaje de asistencia");
                                break;
                        }
                        break;

                    case > 0 and < 71:
                        Console.WriteLine($"NO APROBADO\nTipo de estudiante: " + TipoDeEstudiante + " [ " + mensaje + " ]\nNota obtenida: " + nota);
                        break;

                    default:
                        Console.WriteLine("Error en el dato de nota");
                        break;
                }
                break;
            default:
                Console.WriteLine("Opción no disponible");
                break;
        }

    }

}
//De: Álvaro Rodríguez.    Fecha: 2.9.2026

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
        float nota = 0, PorcentajeDeAsistencia = 0;
        int TipoDeEstudiante = 0;
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

        // if
        if (TipoDeEstudiante == 1 | TipoDeEstudiante == 2 | TipoDeEstudiante == 3)//evaluamos que se haya seleccionado un estudiante
        {
            if (nota >= 71 & nota < 101)
            {//luego validamos la nota y si es mayor a 71 pasamos a evaluar asistencias

                if (PorcentajeDeAsistencia >= 80 & PorcentajeDeAsistencia <= 100)
                {//si asistencia es mayor a 80 aprueba con la nota obtenida
                    Console.WriteLine($"APROBADO\nTipo de estudiante: " + TipoDeEstudiante + " [ " + mensaje + " ]\nNota obtenida: " + nota);

                }
                else if (PorcentajeDeAsistencia >= 60 & PorcentajeDeAsistencia <80)
                {// si asistencia es mayor a 60 aprueba per con 10 decimas abajo

                    if ((nota - 10) >= 71)
                    {
                        Console.WriteLine($"APROBADO\nTipo de estudiante: " + TipoDeEstudiante + " [ " + mensaje + " ]\nNota obtenida: " + (nota - 10));
                    }
                    else if ((nota - 10) < 71)
                    {
                        Console.WriteLine($"NO APROBADO\nTipo de estudiante: " + TipoDeEstudiante + " [ " + mensaje + " ]\nNota obtenida: " + (nota - 10));
                    }

                }
                else if (PorcentajeDeAsistencia >= 1 & PorcentajeDeAsistencia <60)
                {// si asistencia es menor a 59 reprueba por inasistencia
                    Console.WriteLine($"NO APROBADO\nTipo de estudiante: " + TipoDeEstudiante + " [ " + mensaje + " ]\nNota obtenida: " + 0);
                }
                else
                {
                    Console.WriteLine($"Error en el porcentaje de asistencia");
                }

            }
            else if (nota >= 0 & nota < 71)
            {//si la nota es menor a 71 reprueba
                Console.WriteLine($"NO APROBADO\nTipo de estudiante: " + TipoDeEstudiante + " [ " + mensaje + " ]\nNota obtenida: " + nota);
            }
            else
            {
                Console.WriteLine($"Error en el dato de nota");
            }
        }
        else//si no se elige un tipo de estudiante se indica que no existe
        {
            Console.WriteLine($"Opción no disponible");
        }
    }
}
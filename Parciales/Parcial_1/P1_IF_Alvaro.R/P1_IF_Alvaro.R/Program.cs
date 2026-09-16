/*
De: Álvaro Rodríguez - Fecha: 16/9/2026
Problema 1: Sistema de Evaluación de Rendimiento Académico
Desarrollar un programa en C# para un sistema de evaluación académica que reciba como 4 valores de entrada: 
la calificación del primer parcial, la calificación del segundo parcial, el porcentaje de asistencia y el 
nivel del curso (1, 2 o 3). El sistema deberá realizar 4 operaciones: calcular el promedio parcial sumando 
ambas calificaciones y dividiendo entre dos, calcular la bonificación por asistencia multiplicando el 
porcentaje de asistencia por 0.05, calcular la calificación final sumando el promedio parcial con la 
bonificación, y calcular un factor de ajuste según el nivel del curso. Asimismo, debe incluir 3 estructuras 
condicionales (if) con operadores lógicos (&& o ||): una para validar si el alumno reprueba por baja asistencia 
o mal promedio (if (Asistencia < 70 || Promedio < 60)), otra para detectar si es candidato a excelencia 
(if (Calificacion1 >= 90 && Calificacion2 >= 90)), y una última para mención honorífica. Finalmente, el programa 
arrojará 3 valores de salida: la calificación final obtenida, el estado académico y la categoría de certificación asignada.

promedioParcial = (calificacion1 + calificacion2) / 2
bonificacionAsistencia = asistencia * 0.05
calificacionFinal = promedioParcial + bonificacionAsistencia
factorAjuste = calificacionFinal * factorSwitch
 */

internal class Program
{
    private static void Main(string[] args)
    {
        // variables
        int NivelCurso;
        decimal parcial1, parcial2, parcial3, parcial4, PorcentajeAsistencia, promedio = 0, SumaCalificaciones = 0, BonificacionAsistencia =0, PromedioFinal = 0;

        Console.Write($"Ingrese las calificaciones de su parcial 1: "); // las calificaciones de paciales
        parcial1 = decimal.Parse(Console.ReadLine());

        Console.Write($"Ingrese las calificaciones de su parcial 2: "); // las calificaciones de paciales
        parcial2 = decimal.Parse(Console.ReadLine());

        Console.Write($"Ingrese el porcentaje de su asistencia: "); //pide el porcentaje de asistencia
        PorcentajeAsistencia = decimal.Parse(Console.ReadLine());

        Console.Write($"Ingrese su nivel de curso: "); //pide l nivel del curso
        NivelCurso = int.Parse(Console.ReadLine());

        //operaciuones
        SumaCalificaciones = parcial1 + parcial2;
        promedio = SumaCalificaciones / 2;
        BonificacionAsistencia = PorcentajeAsistencia * 0.05m;
        PromedioFinal = promedio + BonificacionAsistencia;
        Console.WriteLine(PromedioFinal+"\n"+ BonificacionAsistencia+"\n"+ PromedioFinal);

        if(NivelCurso == 1 || NivelCurso == 2 || NivelCurso == 3)
        {
            if (PorcentajeAsistencia < 70 || PromedioFinal < 60)
            {
                Console.WriteLine($"Baja asistencia y mal promedio\nPromedio: " + promedio + "\nBonificación asistencia: " + BonificacionAsistencia + "\nPromedioFianl: " + PromedioFinal);
            }
            else if (PromedioFinal >= 90 && PorcentajeAsistencia >= 90)
            {
                Console.WriteLine($"Candidato con buenas calificaciones\nPromedio: " + promedio + "\nBonificación asistencia: " + BonificacionAsistencia + "\nPromedioFianl: " + PromedioFinal);
            }
            else if (PorcentajeAsistencia < 50)
            {
                Console.WriteLine($"Candidato con asistencia baja\nPromedio: " + promedio + "\nBonificación asistencia: " + BonificacionAsistencia + "\nPromedioFianl: " + PromedioFinal);
            }
        }
        else
        {
            Console.WriteLine($"ERROR");
        }
    }
}
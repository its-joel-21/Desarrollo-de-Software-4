using System;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI; // necesario para el openaiclient base

namespace AgenteProblemaWinForms
{
    // fabrica que crea el agente de ia usando groq
    public static class FabricaAgente
    {
        public static AIAgent CrearAgente(string proveedor, string nombre, string instrucciones)
        {
            // instrucciones estrictas para que la ia SOLO devuelva json
            string instruccionesBase =
                "eres un planificador de estudio experto. " +
                "DEBES responder ÚNICAMENTE con un objeto JSON válido, sin texto antes ni después, " +
                "sin bloques de código markdown, sin explicaciones. el formato EXACTO es:\n" +
                "{\n" +
                "  \"materias\": [ { \"nombre\": \"Cálculo I\", \"dificultad\": \"Difícil\", \"color\": \"#81C7D4\" } ],\n" +
                "  \"horario\": [ { \"dia\": 0, \"hora\": 1, \"materia\": \"Cálculo I\", \"color\": \"#81C7D4\" } ],\n" +
                "  \"distribucion\": { \"estudio\": 50, \"descanso\": 30, \"repaso\": 20 },\n" +
                "  \"hitos\": [ \"Examen Cálculo: 15/10\" ],\n" +
                "  \"consejo\": \"usa técnicas de recuerdo activo\",\n" +
                "  \"resumen\": \"explicación breve del plan generado\"\n" +
                "}\n" +
                "REGLAS:\n" +
                "- dia va de 0 (lunes) a 5 (sábado).\n" +
                "- hora va de 1 a 5.\n" +
                "- distribucion debe sumar 100.\n" +
                "- los colores deben ser hexadecimales válidos (#RRGGBB).\n" +
                "- si el usuario no especifica materias, invéntalas según su contexto.\n" +
                "- NUNCA devuelvas texto fuera del json.";

            string promptCompleto = instruccionesBase + "\n" + instrucciones;

            return proveedor switch
            {
                "Groq" => CrearGroq(nombre, promptCompleto),
                _ => throw new ArgumentException("proveedor no válido. solo se acepta groq.")
            };
        }

        private static AIAgent CrearGroq(string nombre, string instrucciones)
        {
            // groq usa una variable de entorno diferente
            string clave = Environment.GetEnvironmentVariable("GROQ_API_KEY")
                ?? throw new InvalidOperationException("falta la variable de entorno GROQ_API_KEY.");

            var cliente = new OpenAIClient(
                new System.ClientModel.ApiKeyCredential(clave),
                new OpenAIClientOptions { Endpoint = new Uri("https://api.groq.com/openai/v1") });

            return cliente.GetChatClient("openai/gpt-oss-120b")
                .AsIChatClient()
                .AsAIAgent(name: nombre, instructions: instrucciones);
        }
    }
}
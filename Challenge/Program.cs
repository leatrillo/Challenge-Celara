using System;
using System.Collections.Generic;
using Challenge;

namespace WordFinderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Definir la matriz de caracteres (cada string representa una fila)
            var matrix = new List<string>
            {
                "coldwind",
                "rainheat",
                "sunnyday",
                "cloudsss",
                "storms  ",
                "foggy   ",
                "weather ",
                "autumn  ",
                "cat     ",
                "onetwo  ",
                "lead    ",
                "double  ",
                "base    "
            };

            // Definir el flujo de palabras (word stream)
            var wordStream = new List<string>
            {
                "cold",
                "wind",
                "rain",
                "heat",
                "sunny",
                "day",
                "cloud",
                "storms",
                "foggy",
                "weather",
                "autumn",
                "winter",
                "spring",
                "summer",
                "cold",    // Palabra duplicada en el flujo
                "rain",
                "rain",    // Palabra duplicada
                "sunny",
                "coldb"    // Palabra vertical
            };

            // Crear instancia de WordFinder
            var wordFinder = new WordFinder(matrix);

            // Buscar las palabras en la matriz
            var result = wordFinder.Find(wordStream);

            // Mostrar los resultados
            Console.WriteLine("Palabras encontradas:");
            foreach (var word in result)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}

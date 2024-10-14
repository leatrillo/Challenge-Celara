using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Challenge
{
    public class WordFinder
    {
        private readonly List<string> matrix;
        private readonly Dictionary<string, int> substrings;

        /// <summary>
        /// Constructor de la clase WordFinder.
        /// </summary>
        /// <param name="matrix">Matriz de caracteres.</param>
        public WordFinder(IEnumerable<string> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            this.matrix = matrix.ToList();

            if (this.matrix.Count == 0)
                throw new ArgumentException("La matriz no puede estar vacía.");

            int rowLength = this.matrix[0].Length;

            if (rowLength == 0)
                throw new ArgumentException("Las filas de la matriz no pueden estar vacías.");

            if (this.matrix.Count > 64 || rowLength > 64)
                throw new ArgumentException("El tamaño de la matriz no puede exceder 64x64.");

            if (this.matrix.Any(row => row.Length != rowLength))
                throw new ArgumentException("Todas las filas de la matriz deben tener la misma longitud.");

            substrings = new Dictionary<string, int>();

            PreprocessMatrix();
        }

        /// <summary>
        /// Preprocesa la matriz extrayendo todas las posibles subcadenas horizontales y verticales.
        /// </summary>
        private void PreprocessMatrix()
        {
            int numRows = matrix.Count;
            int numCols = matrix[0].Length;

            // Procesar filas (horizontal)
            foreach (var row in matrix)
            {
                string trimmedRow = row.Replace(" ", ""); // Eliminar espacios en blanco
                for (int i = 0; i < trimmedRow.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int j = i; j < trimmedRow.Length; j++)
                    {
                        sb.Append(trimmedRow[j]);
                        string substring = sb.ToString();
                        if (substrings.ContainsKey(substring))
                            substrings[substring]++;
                        else
                            substrings[substring] = 1;
                    }
                }
            }

            // Procesar columnas (vertical)
            for (int col = 0; col < numCols; col++)
            {
                StringBuilder columnStringBuilder = new StringBuilder();
                for (int row = 0; row < numRows; row++)
                {
                    char currentChar = matrix[row][col];
                    if (currentChar != ' ') // Ignorar espacios en blanco
                    {
                        columnStringBuilder.Append(currentChar);
                    }
                }
                string columnString = columnStringBuilder.ToString();

                for (int i = 0; i < columnString.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int j = i; j < columnString.Length; j++)
                    {
                        sb.Append(columnString[j]);
                        string substring = sb.ToString();
                        if (substrings.ContainsKey(substring))
                            substrings[substring]++;
                        else
                            substrings[substring] = 1;
                    }
                }
            }
        }

        /// <summary>
        /// Busca las palabras del flujo en la matriz y devuelve las más repetidas.
        /// </summary>
        /// <param name="wordstream">Flujo de palabras a buscar.</param>
        /// <returns>Las palabras encontradas en la matriz.</returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            if (wordstream == null)
                throw new ArgumentNullException(nameof(wordstream));

            // Eliminar duplicados del flujo de palabras
            var uniqueWords = new HashSet<string>(wordstream);

            // Diccionario para almacenar las palabras encontradas y sus conteos
            var wordCounts = new Dictionary<string, int>();

            // Verificar si cada palabra está en los substrings preprocesados
            foreach (var word in uniqueWords)
            {
                if (substrings.TryGetValue(word, out int count))
                {
                    wordCounts[word] = count;
                }
            }

            // Mostrar las palabras y sus conteos
            Console.WriteLine("Palabras encontradas y sus conteos:");
            foreach (var kvp in wordCounts)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            // Obtener todas las palabras encontradas
            var topWords = wordCounts
                .OrderByDescending(kv => kv.Value)
                .Select(kv => kv.Key);

            return topWords;
        }
    }
}

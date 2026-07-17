using System;
using System.Text;

namespace Task2TypesData2
{
    internal class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// Вызывает функцию формирования ромба и выводит результат.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            string diamond = CreateDiamond(5);

            Console.WriteLine(diamond);
        }


        /// <summary>
        /// Формирует ромб из символов X заданного размера.
        /// Центр ромба остается пустым.
        /// </summary>
        /// <param name="n">
        /// Длина диагоналей ромба. Должно быть положительное нечетное число.
        /// </param>
        /// <returns>
        /// Строка с изображением ромба.
        /// </returns>
        static string CreateDiamond(int n)
        {
            StringBuilder result = new StringBuilder();

            int center = n / 2;

            for (int row = 0; row < n; row++)
            {
                for (int column = 0; column < n; column++)
                {
                    // Проверяем, находится ли позиция на границе ромба
                    if (column == Math.Abs(center - row) ||
                        column == n - 1 - Math.Abs(center - row))
                    {
                        result.Append("X");
                    }
                    else
                    {
                        result.Append(" ");
                    }
                }

                result.AppendLine();
            }

            return result.ToString();
        }
    }
}
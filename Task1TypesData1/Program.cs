using System;
using System.Text;

namespace Task1TypesData1
{
    /// <summary>
    /// Главный класс программы.
    /// Выполняет расчет сложных процентов по годам.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// Выполняет вызов функции расчета и выводит результат.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            string result = CalculateCompoundInterest(1000, 3, 10);
            Console.WriteLine(result);
        }

        /// <summary>
        /// Формирует строку с расчетом накоплений по сложным процентам
        /// для каждого года.
        /// </summary>
        /// <param name="initial_deposit">
        /// Начальный вклад.
        /// </param>
        /// <param name="years">
        /// Количество лет расчета.
        /// </param>
        /// <param name="interest_rate">
        /// Годовая процентная ставка.
        /// </param>
        /// <returns>
        /// Строка с суммой накоплений за каждый год.
        /// </returns>
        static string CalculateCompoundInterest(double initial_deposit, int years, double interest_rate)
        {
            StringBuilder result = new StringBuilder();

            double amount = initial_deposit;

            for (int year = 1; year <= years; year++)
            {
                amount = amount * (1 + interest_rate / 100);

                result.AppendLine($"Год {year}: {amount:F2} рублей");
            }

            return result.ToString();
        }
    }
}

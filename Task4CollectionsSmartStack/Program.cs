using System;
using Task4CollectionsSmartStack;

namespace Task4CollectionsSmartStack
{
    internal class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// Демонстрирует работу класса SmartStack.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            Console.WriteLine("=== Создание пустого стека ===");

            SmartStack<int> stack = new SmartStack<int>();

            Console.WriteLine($"Количество элементов: {stack.Count}");
            Console.WriteLine($"Емкость: {stack.Capacity}");

            Console.WriteLine();


            Console.WriteLine("=== Добавление элементов ===");

            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            stack.Push(40);
            stack.Push(50);

            Console.WriteLine($"Количество элементов: {stack.Count}");
            Console.WriteLine($"Емкость после увеличения: {stack.Capacity}");

            Console.WriteLine();


            Console.WriteLine("=== Верхний элемент ===");

            Console.WriteLine(stack.Peek());

            Console.WriteLine();


            Console.WriteLine("=== Проверка Contains ===");

            Console.WriteLine($"Содержит 20: {stack.Contains(20)}");
            Console.WriteLine($"Содержит 100: {stack.Contains(100)}");

            Console.WriteLine();


            Console.WriteLine("=== Перебор через foreach ===");

            foreach (int item in stack)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();


            Console.WriteLine("=== Индексатор ===");

            Console.WriteLine($"Вершина: {stack[0]}");
            Console.WriteLine($"Следующий элемент: {stack[1]}");
            Console.WriteLine($"Основание: {stack[stack.Count - 1]}");

            Console.WriteLine();


            Console.WriteLine("=== Pop ===");

            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());

            Console.WriteLine($"Количество элементов: {stack.Count}");

            Console.WriteLine();


            Console.WriteLine("=== PushRange ===");

            int[] numbers = { 60, 70, 80 };

            stack.PushRange(numbers);

            foreach (int item in stack)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();


            Console.WriteLine("=== Конструктор с емкостью ===");

            SmartStack<string> stack2 = new SmartStack<string>(10);

            Console.WriteLine($"Емкость второго стека: {stack2.Capacity}");

            Console.WriteLine();


            Console.WriteLine("=== Конструктор из коллекции ===");

            string[] words =
            {
                "Один",
                "Два",
                "Три",
                "Четыре"
            };

            SmartStack<string> stack3 = new SmartStack<string>(words);

            foreach (string word in stack3)
            {
                Console.WriteLine(word);
            }
        }
    }
}
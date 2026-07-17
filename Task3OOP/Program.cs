using System;

namespace Task3OOP
{
    /// <summary>
    /// Класс, описывающий товар.
    /// </summary>
    class Product
    {
        /// <summary>
        /// Наименование товара.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Производитель товара.
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Цена товара.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Срок годности товара в днях.
        /// </summary>
        public int ExpirationDate { get; set; }

        /// <summary>
        /// Дата производства товара.
        /// </summary>
        public DateTime ProductionDate { get; set; }


        /// <summary>
        /// Возвращает строковое представление товара.
        /// </summary>
        /// <returns>
        /// Информация о товаре.
        /// </returns>
        public override string ToString()
        {
            return $"Товар: {Name}\n" +
                   $"Производитель: {Manufacturer}\n" +
                   $"Цена: {Price:F2} руб.\n" +
                   $"Срок годности: {ExpirationDate} дней\n" +
                   $"Дата производства: {ProductionDate:dd.MM.yyyy}";
        }
    }


    internal class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// Создает объект товара и выводит его информацию.
        /// </summary>
        static void Main(string[] args)
        {
            Console.Write("Введите название товара: ");
            string name = Console.ReadLine();


            Console.Write("Введите производителя: ");
            string manufacturer = Console.ReadLine();


            Console.Write("Введите цену: ");
            double price = Convert.ToDouble(Console.ReadLine());


            Console.Write("Введите срок годности (дни): ");
            int expirationDate = Convert.ToInt32(Console.ReadLine());


            Console.Write("Введите дату производства (дд.мм.гггг): ");
            DateTime productionDate = DateTime.Parse(Console.ReadLine());


            Product product = new Product()
            {
                Name = name,
                Manufacturer = manufacturer,
                Price = price,
                ExpirationDate = expirationDate,
                ProductionDate = productionDate
            };


            Console.WriteLine("\nИнформация о товаре:");
            Console.WriteLine(product);
        }
    }
}
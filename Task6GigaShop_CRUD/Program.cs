using Task6GigaShop_CRUD.EF;
using Task6GigaShop_CRUD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task6GigaShop_CRUD.ADO;
using Task6GigaShop_CRUD.Data;


// ==============================
// Загрузка конфигурации
// ==============================

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();



string connectionString =
    configuration.GetConnectionString("DefaultConnection")
    ?? throw new Exception("Нет строки подключения");



// ==============================
// Настройка EF
// ==============================

var services = new ServiceCollection();


services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});


var provider = services.BuildServiceProvider();



var context = provider.GetRequiredService<AppDbContext>();



var adoRepository = new ProductRepository(connectionString);


var efService = new ProductService(context);



// ==============================
// Главное меню
// ==============================

while (true)
{

    Console.Clear();

    Console.WriteLine("===============================");
    Console.WriteLine("       GigaShop CRUD");
    Console.WriteLine("===============================");

    Console.WriteLine("1. ADO.NET");

    Console.WriteLine("2. Entity Framework");

    Console.WriteLine("0. Выход");


    Console.Write("\nВыберите: ");

    string? choice = Console.ReadLine();



    switch (choice)
    {

        case "1":

            AdoMenu(adoRepository);

            break;



        case "2":

            EfMenu(efService);

            break;



        case "0":

            return;



        default:

            Console.WriteLine("Неверный выбор");

            Console.ReadKey();

            break;
    }

}





// =====================================
// Меню ADO.NET
// =====================================

static void AdoMenu(ProductRepository repository)
{

    while (true)
    {

        Console.Clear();


        Console.WriteLine("========= ADO.NET =========");


        Console.WriteLine("1. Показать товары");

        Console.WriteLine("2. Добавить товар");

        Console.WriteLine("0. Назад");


        Console.Write("\nВыбор: ");


        string? choice = Console.ReadLine();



        switch (choice)
        {

            case "1":

                repository.GetProducts();

                Console.ReadKey();

                break;



            case "2":

                Console.WriteLine(
                    "Добавление товара будет подключено после выбора ID категории и производителя"
                );

                Console.ReadKey();

                break;



            case "0":

                return;



            default:

                Console.WriteLine("Ошибка");

                Console.ReadKey();

                break;

        }

    }

}





// =====================================
// Меню Entity Framework
// =====================================

static void EfMenu(ProductService service)
{

    while (true)
    {

        Console.Clear();


        Console.WriteLine("====== Entity Framework ======");


        Console.WriteLine("1. Показать товары");

        Console.WriteLine("2. Добавить товар");

        Console.WriteLine("0. Назад");


        Console.Write("\nВыбор: ");


        string? choice = Console.ReadLine();



        switch (choice)
        {

            case "1":

                service.GetProducts();

                Console.ReadKey();

                break;



            case "2":

                Console.WriteLine(
                    "Добавление товара через EF будет подключено после выбора ID категории и производителя"
                );

                Console.ReadKey();

                break;



            case "0":

                return;



            default:

                Console.WriteLine("Ошибка");

                Console.ReadKey();

                break;

        }

    }

}
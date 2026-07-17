using Microsoft.EntityFrameworkCore;
using Task6GigaShop_CRUD.Data;
using Task6GigaShop_CRUD.Models;

namespace Task6GigaShop_CRUD.EF;

public class ProductService
{
    private readonly AppDbContext _context;


    public ProductService(AppDbContext context)
    {
        _context = context;
    }



    // ============================
    // CREATE
    // Добавление товара через EF
    // ============================

    public void CreateProduct(Product product)
    {
        _context.Products.Add(product);

        _context.SaveChanges();
    }



    // ============================
    // READ
    // Получение товаров через EF
    // ============================

    public void GetProducts()
    {
        var products = _context.Products
            .Include(x => x.Category)
            .Include(x => x.Manufacturer)
            .ToList();



        Console.WriteLine("\n===== СПИСОК ТОВАРОВ EF =====\n");


        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}");

            Console.WriteLine($"Название: {product.Name}");

            Console.WriteLine($"Цена: {product.Price} руб.");

            Console.WriteLine($"Категория: {product.Category.Name}");

            Console.WriteLine($"Производитель: {product.Manufacturer.Name}");

            Console.WriteLine($"Доступен: {product.IsAvailable}");

            Console.WriteLine("-------------------------");
        }
    }
}
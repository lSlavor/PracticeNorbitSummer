using Npgsql;

namespace Task6GigaShop_CRUD.ADO;

public class ProductRepository
{
    private readonly string _connectionString;


    public ProductRepository(string connectionString)
    {
        _connectionString = connectionString;
    }



    // CREATE
    // Добавление нового товара

    public void CreateProduct(
        Guid categoryId,
        Guid manufacturerId,
        string name,
        decimal price)
    {

        using var connection = new NpgsqlConnection(_connectionString);

        connection.Open();


        string sql = @"
            INSERT INTO products
            (
                category_id,
                manufacturer_id,
                name,
                price
            )
            VALUES
            (
                @categoryId,
                @manufacturerId,
                @name,
                @price
            );
        ";



        using var command = new NpgsqlCommand(sql, connection);


        command.Parameters.AddWithValue(
            "categoryId",
            categoryId);


        command.Parameters.AddWithValue(
            "manufacturerId",
            manufacturerId);


        command.Parameters.AddWithValue(
            "name",
            name);


        command.Parameters.AddWithValue(
            "price",
            price);



        command.ExecuteNonQuery();
    }
    // READ
    // Получение всех товаров

    public void GetProducts()
    {
        using var connection = new NpgsqlConnection(_connectionString);

        connection.Open();


        string sql = @"
        SELECT 
            p.id,
            p.name,
            p.price,
            p.created_at,
            p.is_available,
            c.name AS category,
            m.name AS manufacturer

        FROM products p

        INNER JOIN categories c
            ON p.category_id = c.id

        INNER JOIN manufacturers m
            ON p.manufacturer_id = m.id;
    ";


        using var command = new NpgsqlCommand(sql, connection);


        using var reader = command.ExecuteReader();



        Console.WriteLine("\n===== СПИСОК ТОВАРОВ =====\n");



        while (reader.Read())
        {
            Console.WriteLine(
                $"ID: {reader["id"]}"
            );


            Console.WriteLine(
                $"Название: {reader["name"]}"
            );


            Console.WriteLine(
                $"Цена: {reader["price"]} руб."
            );


            Console.WriteLine(
                $"Категория: {reader["category"]}"
            );


            Console.WriteLine(
                $"Производитель: {reader["manufacturer"]}"
            );


            Console.WriteLine(
                $"Доступен: {reader["is_available"]}"
            );


            Console.WriteLine(
                $"Создан: {reader["created_at"]}"
            );


            Console.WriteLine("-------------------------");
        }
    }
}
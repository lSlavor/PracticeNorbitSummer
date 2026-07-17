using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task6GigaShop_CRUD.Models;

[Table("order_items")]
public class OrderItem
{
    // Составной первичный ключ
    [Column("order_id")]
    public Guid OrderId { get; set; }


    [Column("product_id")]
    public Guid ProductId { get; set; }


    [Required]
    [Column("quantity")]
    public int Quantity { get; set; }


    [Required]
    [Column("price", TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }



    // Связь с заказом
    public Order Order { get; set; } = null!;


    // Связь с товаром
    public Product Product { get; set; } = null!;
}
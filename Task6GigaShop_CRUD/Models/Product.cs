using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task6GigaShop_CRUD.Models;

[Table("products")]
public class Product
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }


    // Внешний ключ категории
    [Required]
    [Column("category_id")]
    public Guid CategoryId { get; set; }


    // Внешний ключ производителя
    [Required]
    [Column("manufacturer_id")]
    public Guid ManufacturerId { get; set; }


    [Required]
    [MaxLength(150)]
    [Column("name")]
    public string Name { get; set; } = null!;


    [Required]
    [Column("price", TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }


    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;


    [Column("is_available")]
    public bool IsAvailable { get; set; } = true;



    // Навигация к категории
    public Category Category { get; set; } = null!;


    // Навигация к производителю
    public Manufacturer Manufacturer { get; set; } = null!;


    // Связь товар -> позиции заказов
    public ICollection<OrderItem> OrderItems { get; set; }
        = new List<OrderItem>();
}
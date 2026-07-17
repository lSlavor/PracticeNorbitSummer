using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task6GigaShop_CRUD.Models;

[Table("categories")]
public class Category
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }


    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = null!;


    [Column("description")]
    public string? Description { get; set; }


    // Связь: одна категория -> много товаров
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
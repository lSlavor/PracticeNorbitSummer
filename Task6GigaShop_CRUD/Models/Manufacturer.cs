using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task6GigaShop_CRUD.Models;

[Table("manufacturers")]
public class Manufacturer
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }


    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = null!;


    [MaxLength(100)]
    [Column("country")]
    public string? Country { get; set; }


    [Column("founded_at")]
    public DateTime? FoundedAt { get; set; }


    [Column("is_active")]
    public bool IsActive { get; set; } = true;


    // Связь один производитель -> много товаров
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
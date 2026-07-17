using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task6GigaShop_CRUD.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }


    [Required]
    [MaxLength(150)]
    [Column("full_name")]
    public string FullName { get; set; } = null!;


    [Required]
    [MaxLength(150)]
    [Column("email")]
    public string Email { get; set; } = null!;


    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;


    [Column("is_admin")]
    public bool IsAdmin { get; set; } = false;



    // Один пользователь -> много заказов
    public ICollection<Order> Orders { get; set; }
        = new List<Order>();
}
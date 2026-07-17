using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task6GigaShop_CRUD.Models;

[Table("orders")]
public class Order
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }


    // Внешний ключ пользователя
    [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }


    [Required]
    [Column("order_date")]
    public DateTime OrderDate { get; set; } = DateTime.Now;


    [Column("total_price")]
    public decimal? TotalPrice { get; set; }


    [Column("is_paid")]
    public bool IsPaid { get; set; } = false;



    // Пользователь, который сделал заказ
    public User User { get; set; } = null!;


    // Состав заказа
    public ICollection<OrderItem> OrderItems { get; set; }
        = new List<OrderItem>();
}
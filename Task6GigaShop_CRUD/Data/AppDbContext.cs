using Task6GigaShop_CRUD.Models;
using Microsoft.EntityFrameworkCore;
namespace Task6GigaShop_CRUD.Data;

public class AppDbContext : DbContext
{
    public DbSet<Manufacturer> Manufacturers { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }



    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        // Настройка составного ключа order_items

        modelBuilder.Entity<OrderItem>()
            .HasKey(x => new
            {
                x.OrderId,
                x.ProductId
            });



        // Связь Product -> Category

        modelBuilder.Entity<Product>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);



        // Связь Product -> Manufacturer

        modelBuilder.Entity<Product>()
            .HasOne(x => x.Manufacturer)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.ManufacturerId);



        // Связь Order -> User

        modelBuilder.Entity<Order>()
            .HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId);



        // Связь OrderItem -> Order

        modelBuilder.Entity<OrderItem>()
            .HasOne(x => x.Order)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.OrderId);



        // Связь OrderItem -> Product

        modelBuilder.Entity<OrderItem>()
            .HasOne(x => x.Product)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.ProductId);

    }
}
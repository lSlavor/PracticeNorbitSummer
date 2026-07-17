using Microsoft.EntityFrameworkCore;
using TimeTrackingApi.Models;

namespace TimeTrackingApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<TaskItem> Tasks { get; set; } = null!;
    public DbSet<TimeEntry> TimeEntries { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ---------- projects ----------
        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("projects");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            entity.Property(p => p.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
            entity.Property(p => p.IsActive).HasColumnName("is_active");
        });

        // ---------- tasks ----------
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.ToTable("tasks");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("id");
            entity.Property(t => t.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            entity.Property(t => t.IsActive).HasColumnName("is_active");
            entity.Property(t => t.ProjectId).HasColumnName("project_id");

            entity.HasOne(t => t.Project)
                  .WithMany(p => p.Tasks)
                  .HasForeignKey(t => t.ProjectId);
        });

        // ---------- time_entries ----------
        modelBuilder.Entity<TimeEntry>(entity =>
        {
            entity.ToTable("time_entries");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("id");
            entity.Property(t => t.EntryDate).HasColumnName("entry_date");
            entity.Property(t => t.Hours).HasColumnName("hours").HasColumnType("numeric(4,2)");
            entity.Property(t => t.Description).HasColumnName("description").HasMaxLength(300);
            entity.Property(t => t.EmployeeName).HasColumnName("employee_name").HasMaxLength(150).IsRequired();
            entity.Property(t => t.TaskId).HasColumnName("task_id");

            entity.HasOne(t => t.Task)
                  .WithMany(task => task.TimeEntries)
                  .HasForeignKey(t => t.TaskId);
        });
    }
}

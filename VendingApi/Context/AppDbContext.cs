using Microsoft.EntityFrameworkCore;
using VendingApi.Models;

namespace VendingApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public virtual DbSet<Machines> Machines { get; set; }
    public virtual DbSet<Maintenances> Maintenances { get; set; }
    public virtual DbSet<Products> Products { get; set; }
    public virtual DbSet<Sales> Sales { get; set; }
    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<Sessions> Sessions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // По умолчанию
        modelBuilder.Entity<Users>()
            .Property(u => u.Role)
            .HasDefaultValue(UserRole.Default)
            .HasSentinel(UserRole.Default);
        modelBuilder.Entity<Machines>()
            .Property(u => u.EntryDate)
            .HasDefaultValueSql("now() at time zone 'utc'");
        modelBuilder.Entity<Machines>()
            .Property(u => u.TotalEarn)
            .HasDefaultValue(0);
        modelBuilder.Entity<Sales>()
            .Property(u => u.Date)
            .HasDefaultValueSql("now() at time zone 'utc'");
        modelBuilder.Entity<Sessions>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("now() at time zone 'utc'");
        
        // Ограничения
        modelBuilder.Entity<Machines>()
            .ToTable(t => t.HasCheckConstraint("Machines_StartDate_check",
                "\"StartDate\" >= \"EntryDate\" and \"StartDate\" > \"ManufactureDate\""));
        modelBuilder.Entity<Machines>()
            .ToTable(t => t.HasCheckConstraint("Machines_LastInspectionDate_check",
                "\"LastInspectionDate\" >= \"EntryDate\" and \"LastInspectionDate\" <= now()"));
        modelBuilder.Entity<Machines>()
            .ToTable(t => t.HasCheckConstraint("Machines_LastInspectionDate_check",
                "\"NextMaintenanceDate\" >= \"EntryDate\" and \"NextMaintenanceDate\" <= now()"));
            
        base.OnModelCreating(modelBuilder);
    }
}

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
        modelBuilder.Entity<Machines>()
            .Property(u => u.Status)
            .HasDefaultValue(MachineStatus.Operational)
            .HasSentinel(MachineStatus.Operational);
        modelBuilder.Entity<Machines>()
            .Property(u => u.Timezone)
            .HasDefaultValue(MachineTimezone.UTC3)
            .HasSentinel(MachineTimezone.UTC3);
        modelBuilder.Entity<Machines>()
            .Property(u => u.Priority)
            .HasDefaultValue(MachinePriority.Medium)
            .HasSentinel(MachinePriority.Medium);
        modelBuilder.Entity<Machines>()
            .Property(u => u.WorkMode)
            .HasDefaultValue(MachineWorkMode.Standart)
            .HasSentinel(MachineWorkMode.Standart);
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
            .ToTable(t => t.HasCheckConstraint("Machines_LastMaintenanceDate_check",
                "\"LastMaintenanceDate\" >= \"EntryDate\" and \"LastMaintenanceDate\" <= now()"));
        modelBuilder.Entity<Machines>()
            .ToTable(t => t.HasCheckConstraint("Machines_NextMaintenanceDate_check",
                "\"NextMaintenanceDate\" >= \"EntryDate\" and \"NextMaintenanceDate\" <= now()"));
            
        base.OnModelCreating(modelBuilder);
    }
}

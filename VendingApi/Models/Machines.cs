using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VendingApi.Models;

[Index(nameof(Name))]
[Index(nameof(SerialNumber), IsUnique = true)]
[Index(nameof(InventoryNumber), IsUnique = true)]
public class Machines
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    [MinLength(5)]
    [MaxLength(100)]
    public required string Location { get; set; }
    
    [MinLength(2)]
    [MaxLength(40)]
    public required string Model { get; set; }
    
    public MachinePaymentType PaymentType { get; set; }
    
    [Range(0, int.MaxValue)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal TotalEarn { get; set; }

    [MinLength(1)]
    [MaxLength(40)]
    public required string SerialNumber { get; set; }
    
    [MinLength(1)]
    [MaxLength(40)]
    public required string InventoryNumber { get; set; }
    
    [MinLength(1)]
    [MaxLength(100)]
    public required string Manufacturer { get; set; }
    
    public required string Modem { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTimeOffset EntryDate { get; set; }
    public DateTimeOffset ManufactureDate { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? LastInspectionDate { get; set; }
    public DateTimeOffset? NextMaintenanceDate { get; set; }
    
    [Range(0, int.MaxValue)]
    public int InspectionIntervalMonths { get; set; }
    
    [Range(0, int.MaxValue)]
    public int ResourceHours { get; set; }
    
    [Range(1, 20)]
    public int MaintenanceHours { get; set; }
    
    public MachineStatus Status { get; set; }
    
    [MinLength(2)]
    [MaxLength(100)]
    public required string OriginCounty { get; set; }
    
    public int? LastInspectedById { get; set; }

    [ForeignKey(nameof(LastInspectedById))]
    public Users? LastInspectedBy { get; set; }
    
    public virtual ICollection<Sales> Sales { get; set; } = [];
    public virtual ICollection<Maintenances> Maintenances { get ; set; } = [];
}

public enum MachinePaymentType
{
    Cash,
    Card,
    CashAndCard
}

public enum MachineStatus
{
    Operational,
    Broken,
    InService
}

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

    public int ModelId { get; set; }

    [ForeignKey(nameof(ModelId))] public MachineModels Model { get; set; }

    public required string Name { get; set; }

    public required string Location { get; set; }

    public required string Address { get; set; }

    public string? Coordinates { get; set; }

    public MachinePaymentType PaymentType { get; set; }

    [Range(0, int.MaxValue)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal TotalEarn { get; set; }

    [MinLength(1)] [MaxLength(40)] public string SerialNumber { get; set; }

    [MinLength(1)] [MaxLength(40)] public string InventoryNumber { get; set; }

    public string? Modem { get; set; }

    [MinLength(11)]
    [MaxLength(11)] // 00:00-00:00
    public string? WorkTime { get; set; }

    public MachineTimezone Timezone { get; set; }

    public MachinePriority Priority { get; set; }

    public MachineWorkMode WorkMode { get; set; }

    [MaxLength(1000)] public string? Notes { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTimeOffset EntryDate { get; set; }

    public DateTimeOffset ManufactureDate { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? LastMaintenanceDate { get; set; }
    public DateTimeOffset? NextMaintenanceDate { get; set; }

    [Range(0, int.MaxValue)] public int? InspectionIntervalMonths { get; set; }

    [Range(0, int.MaxValue)] public int? ResourceHours { get; set; }

    public MachineStatus Status { get; set; } = MachineStatus.Operational;

    public int? LastMaintenanceId { get; set; }

    [ForeignKey(nameof(LastMaintenanceId))] public Maintenances? LastMaintenance { get; set; }

    public virtual ICollection<Sales> Sales { get; set; } = [];
    public virtual ICollection<Maintenances> Maintenances { get; set; } = [];
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

public enum MachineWorkMode
{
    Standart
}

public enum MachineTimezone
{
    UTC_12,
    UTC_11,
    UTC_10,
    UTC_9,
    UTC_8,
    UTC_7,
    UTC_6,
    UTC_5,
    UTC_4,
    UTC_3,
    UTC_2,
    UTC_1,
    UTC_0,
    UTC1,
    UTC2,
    UTC3,
    UTC4,
    UTC5,
    UTC6,
    UTC7,
    UTC8,
    UTC9,
    UTC10,
    UTC11,
    UTC12,
}

public enum MachinePriority
{
    High,
    Medium,
    Low
}
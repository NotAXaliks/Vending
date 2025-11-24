using System.ComponentModel.DataAnnotations;
using VendingApi.Models;

namespace VendingApi.Dtos;

public record MachineDto(
    int Id,
    string Name,
    string Location,
    string Model,
    MachinePaymentType PaymentType,
    decimal TotalEarn,
    string SerialNumber,
    string InventoryNumber,
    string Manufacturer,
    string Modem,
    long EntryDate,
    long ManufactureDate,
    long StartDate,
    long? LastInspectionDate,
    long? NextMaintenanceDate,
    int InspectionIntervalMonths,
    int ResourceHours,
    int MaintenanceHours,
    MachineStatus Status,
    string OriginCounty,
    int? LastInspectedById);

// Передаваемые данные

public record GetMachinesResponse(MachineDto[] Machines, int FoundCount, int TotalCount);

// Получаемые данные

public class GetMachinesRequestDto
{
    public int? Page { get; set; }
    public int? Show { get; set; }
    public string? Filter { get; set; }
}

public class CreateMachineRequestDto
{
    [Required] [MaxLength(50)] public required string Name { get; set; }

    [Required] public required string Manufacturer { get; set; }

    [Required] public required string Model { get; set; }

    [Required] public required string WorkMode { get; set; }

    [Required] public required string Address { get; set; }

    [Required] public required string Location { get; set; }

    public required string Coordinates { get; set; }

    [Required] public required string MachineNumber { get; set; }

    public required string WorkTime { get; set; }

    [Required]
    public required MachineTimezone Timezone { get; set; }
    
    [Required]
    public required MachinePriority Priority { get; set; }

    // хз
    // public required string ProductMatrix { get; set; }
    // [Required]
}

public enum MachinePriority
{
    High,
    Medium,
    Low
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

using System.ComponentModel.DataAnnotations;
using VendingApi.Models;

namespace VendingApi.Dtos;

public record MachineModelDto(int Id, string Name, string Manufacturer, string OriginCounty);

public record MachineDto(
    int Id,
    int ModelId,
    string Name,
    string Location,
    string Address,
    string? Coordinates,
    MachinePaymentType PaymentType,
    decimal TotalEarn,
    string SerialNumber,
    string InventoryNumber,
    string? Modem,
    string? WorkTime,
    MachineTimezone Timezone,
    MachinePriority Priority,
    MachineWorkMode WorkMode,
    string? Notes,
    long EntryDate,
    long StartDate,
    long ManufactureDate,
    long? LastMaintenanceDate,
    long? NextMaintenanceDate,
    int? InspectionIntervalMonths,
    int? ResourceHours,
    MachineStatus Status,
    int? LastMaintenanceId);

public record MachineWithModelDto(
    int Id,
    MachineModelDto ModelDto,
    string Name,
    string Location,
    MachinePaymentType PaymentType,
    decimal TotalEarn,
    string SerialNumber,
    string InventoryNumber,
    string? Modem,
    string? WorkTime,
    MachineTimezone Timezone,
    MachinePriority Priority,
    MachineWorkMode WorkMode,
    string? Notes,
    long EntryDate,
    long StartDate,
    long ManufactureDate,
    long? LastMaintenanceDate,
    long? NextMaintenanceDate,
    int? InspectionIntervalMonths,
    int? ResourceHours,
    MachineStatus Status,
    int? LastMaintenanceId);

// Передаваемые данные

public record GetMachinesResponse(MachineWithModelDto[] Machines, int FoundCount, int TotalCount);

// Получаемые данные

public class GetMachinesRequestDto
{
    public int? Page { get; set; }
    public int? Show { get; set; }
    public string? Filter { get; set; }
}

public class CreateMachineRequestDto
{
    [Required] [Range(0, int.MaxValue)] public int ModelId { get; set; }
    
    [Required] [MaxLength(50)] public required string Name { get; set; }

    [Required] public required string Location { get; set; }

    [Required] public required string Address { get; set; }

    public string? Coordinates { get; set; }
    
    public required MachinePaymentType PaymentType { get; set; }

    [Required] public required string SerialNumber { get; set; }
    [Required] public required string InventoryNumber { get; set; }
    
    public string? Modem { get; set; }

    public required string WorkTime { get; set; }

    public required MachineTimezone Timezone { get; set; }

    public required MachinePriority Priority { get; set; }

    public required MachineWorkMode WorkMode { get; set; }

    public string? Notes { get; set; }
    
    public int ManufactureDate { get; set; }

    // хз
    // public required string ProductMatrix { get; set; }
    // [Required]
}
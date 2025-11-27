using System.Text.Json.Serialization;

namespace VendingDesktop.Dtos;

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
    long ManufactureDate,
    long StartDate,
    long? LastMaintenanceDate,
    long? NextMaintenanceDate,
    int? InspectionIntervalMonths,
    int? ResourceHours,
    MachineStatus Status,
    int? LastMaintenanceId);

public record MachineWithModelDto(
    int Id,
    MachineModelDto Model,
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
    long ManufactureDate,
    long StartDate,
    long? LastMaintenanceDate,
    long? NextMaintenanceDate,
    int? InspectionIntervalMonths,
    int? ResourceHours,
    MachineStatus Status,
    int? LastMaintenanceId);

[JsonConverter(typeof(JsonStringEnumConverter<MachinePaymentType>))]
public enum MachinePaymentType
{
    Cash,
    Card,
    CashAndCard
}

[JsonConverter(typeof(JsonStringEnumConverter<MachineStatus>))]
public enum MachineStatus
{
    Operational,
    Broken,
    InService
}

[JsonConverter(typeof(JsonStringEnumConverter<MachineWorkMode>))]
public enum MachineWorkMode
{
    Standart
}

[JsonConverter(typeof(JsonStringEnumConverter<MachineTimezone>))]
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

[JsonConverter(typeof(JsonStringEnumConverter<MachinePriority>))]
public enum MachinePriority
{
    High,
    Medium,
    Low
}

// Получаемые данные

public record GetMachinesResponse(MachineWithModelDto[] Machines, int FoundCount, int TotalCount);

// Передаваемые данные

public class GetMachinesRequest
{
    public string? Filter { get; set; }
    public int? Show { get; set; }
    public int? Page { get; set; }
}

public class CreateMachineRequest
{
    public int ModelId { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    public required string Address { get; set; }
    public string? Coordinates { get; set; }
    public required MachinePaymentType PaymentType { get; set; }
    public required string SerialNumber { get; set; }
    public required string InventoryNumber { get; set; }
    public string? Modem { get; set; }
    public required string WorkTime { get; set; }
    public required MachineTimezone Timezone { get; set; }
    public required MachinePriority Priority { get; set; }
    public required MachineWorkMode WorkMode { get; set; }
    public string? Notes { get; set; }
    public long ManufactureDate { get; set; }
    public long? NextMaintenanceDate { get; set; }
}
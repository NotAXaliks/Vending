using System.Text.Json.Serialization;

namespace VendingDesktop.Dtos;

public record MachineDto(
    int Id,
    int ModelId,
    string Name,
    string Location,
    MachinePaymentType PaymentType,
    decimal TotalEarn,
    string SerialNumber,
    string InventoryNumber,
    string Modem,
    string? WorkTime,
    MachineTimezone Timezone,
    MachinePriority Priority,
    MachineWorkMode WorkMode,
    string? Notes,
    long EntryDate,
    long ManufactureDate,
    long StartDate,
    long? LastInspectionDate,
    long? NextMaintenanceDate,
    int InspectionIntervalMonths,
    int ResourceHours,
    int MaintenanceHours,
    MachineStatus Status,
    int? LastInspectedById);

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

public class GetMachinesResponse
{
    public required MachineDto[] Machines { get; set; }
    public int FoundCount { get; set; }
    public int TotalCount { get; set; }
}

// Передаваемые данные

public class GetMachinesRequest
{
    public string? Filter { get; set; }
    public int? Show { get; set; }
    public int? Page { get; set; }
}
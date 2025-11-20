using System.Text.Json.Serialization;

namespace VendingDesktop.Dtos;

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

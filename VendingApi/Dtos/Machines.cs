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
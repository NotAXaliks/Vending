using System.ComponentModel.DataAnnotations;
using VendingApi.Models;

namespace VendingApi.Dtos;

// Передаваемые данные

public record MachineDto(
    int Id,
    string Location,
    string Model,
    MachinePaymentType PaymentType,
    decimal Price,
    string SerialNumber,
    string InventoryNumber,
    string Manufacter,
    long EntryDate,
    long ManufactureDate,
    long StartDate,
    long LastInspectionDate,
    long NextMaintenanceDate,
    int InspectionIntervalMonths,
    int ResourceHours,
    int MaintenanceHours,
    MachineStatus Status,
    string OriginCounty,
    int? LastInspectedById);

// Получаемые данные

public class GetMachinesRequestDto
{
    public int? Page { get; set; }
    public int? Show { get; set; }
    public string? Filter { get; set; }
}
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendingApi.Context;
using VendingApi.Dtos;
using VendingApi.Models;

namespace VendingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class MachinesController(AppDbContext databaseContext, IConfiguration configuration) : BaseController(
    databaseContext,
    configuration)
{
    [HttpPost]
    public async Task<IActionResult> ListMachines([FromBody] GetMachinesRequestDto dto)
    {
        // var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        // if (idClaim == null) return Unauthorized("Invalid token");
        //
        // var user = await DatabaseContext.Users.FindAsync(idClaim);
        // if (user == null) return Unauthorized("Invalid token");

        /* Валидация */
        if (!ModelState.IsValid) return SendError("Invalid data");
        /* Конец валидации */

        var takeMachines = dto.Show ?? 10;
        var page = dto.Page ?? 1;

        var machinesQuery = DatabaseContext.Machines.Include(m => m.Model).AsQueryable();

        if (!string.IsNullOrWhiteSpace(dto.Filter))
        {
            machinesQuery = machinesQuery.Where(m => m.Name.ToUpper().Contains(dto.Filter.ToUpper()));
        }

        var foundMachinesCount = await machinesQuery.CountAsync();
        if (page > 0) machinesQuery = machinesQuery.Skip(takeMachines * (page - 1));

        var totalMachinesCount = await DatabaseContext.Machines.CountAsync();
        var machines = await machinesQuery.Take(takeMachines).ToListAsync();

        var result = machines.Select(m => new MachineWithModelDto(
            Id: m.Id,
            Name: m.Name,
            Location: m.Location,
            Address: m.Address,
            Coordinates: m.Coordinates,
            PaymentType: m.PaymentType,
            TotalEarn: m.TotalEarn,
            SerialNumber: m.SerialNumber,
            InventoryNumber: m.InventoryNumber,
            Modem: m.Modem,
            WorkTime: m.WorkTime,
            Timezone: m.Timezone,
            Priority: m.Priority,
            WorkMode: m.WorkMode,
            Notes: m.Notes,
            EntryDate: m.EntryDate.ToUnixTimeSeconds(),
            ManufactureDate: m.ManufactureDate.ToUnixTimeSeconds(),
            StartDate: m.StartDate.ToUnixTimeSeconds(),
            LastMaintenanceDate: m.LastMaintenanceDate?.ToUnixTimeSeconds(),
            NextMaintenanceDate: m.NextMaintenanceDate?.ToUnixTimeSeconds(),
            InspectionIntervalMonths: m.InspectionIntervalMonths,
            ResourceHours: m.ResourceHours,
            Status: m.Status,
            LastMaintenanceId: m.LastMaintenanceId,
            Model: new MachineModelDto(
                Id: m.Model.Id,
                Name: m.Model.Name,
                Manufacturer: m.Model.Manufacturer,
                OriginCounty: m.Model.Country
            )
        )).ToArray();

        return SendData(new GetMachinesResponse(result, foundMachinesCount, totalMachinesCount));
    }

    [HttpPut]
    public async Task<IActionResult> CreateMachine([FromBody] CreateMachineRequestDto dto)
    {
        // var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        // if (idClaim == null) return Unauthorized("Invalid token");
        //
        // var user = await DatabaseContext.Users.FindAsync(idClaim);
        // if (user == null) return Unauthorized("Invalid token");

        /* Валидация */
        if (!ModelState.IsValid) return SendError("Invalid data");
        if (!string.IsNullOrWhiteSpace(dto.WorkTime) &&
            Regex.IsMatch(dto.WorkTime, @"(?:[01]\d|2[0-3]):[0-5]\d-[0-5]\d:[0-5]\d"))
            return SendError("Invalid WorkTime");
        /* Конец валидации */

        var model = await DatabaseContext.MachineModels.FindAsync(dto.ModelId);
        if (model == null) return SendError("Model not found");

        var machine = new Machines
        {
            ModelId = dto.ModelId,
            Name = dto.Name,
            Location = dto.Location,
            Address = dto.Address,
            Coordinates = dto.Coordinates,
            PaymentType = dto.PaymentType,
            SerialNumber = dto.SerialNumber,
            InventoryNumber = dto.InventoryNumber,
            Modem = dto.Modem,
            WorkTime = dto.WorkTime,
            Timezone = dto.Timezone,
            Priority = dto.Priority,
            WorkMode = dto.WorkMode,
            Notes = dto.Notes,
            ManufactureDate = DateTimeOffset.FromUnixTimeMilliseconds(dto.ManufactureDate),
            NextMaintenanceDate = dto.NextMaintenanceDate is { } date ? DateTimeOffset.FromUnixTimeMilliseconds(date) : null,
        };

        await DatabaseContext.Machines.AddAsync(machine);
        await DatabaseContext.SaveChangesAsync();

        return Ok();
    }
}
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendingApi.Context;
using VendingApi.Dtos;

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

        var machinesQuery = DatabaseContext.Machines.AsQueryable();

        if (!string.IsNullOrWhiteSpace(dto.Filter))
        {
            machinesQuery = machinesQuery.Where(m => m.Name.ToUpper().Contains(dto.Filter.ToUpper()));
        }

        var foundMachinesCount = await machinesQuery.CountAsync();
        if (page > 0) machinesQuery = machinesQuery.Skip(takeMachines * (page - 1));

        var totalMachinesCount = await DatabaseContext.Machines.CountAsync();
        var machines = await machinesQuery.Take(takeMachines).ToListAsync();

        var formattedMachines = machines.Select(m => new MachineDto(m.Id, m.Name, m.Location, m.Model, m.PaymentType,
            m.TotalEarn, m.SerialNumber, m.InventoryNumber, m.Manufacturer, m.Modem,
            m.EntryDate.ToUnixTimeMilliseconds(), m.ManufactureDate.ToUnixTimeMilliseconds(),
            m.StartDate.ToUnixTimeMilliseconds(), m.LastInspectionDate?.ToUnixTimeMilliseconds(),
            m.NextMaintenanceDate?.ToUnixTimeMilliseconds(), m.InspectionIntervalMonths, m.ResourceHours,
            m.MaintenanceHours, m.Status, m.OriginCounty, m.LastInspectedById)).ToArray();

        return SendData(new GetMachinesResponse(formattedMachines, foundMachinesCount, totalMachinesCount));
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

        return Ok();
    }
}
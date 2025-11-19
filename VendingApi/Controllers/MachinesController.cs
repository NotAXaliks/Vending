using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendingApi.Context;
using VendingApi.Dtos;

namespace VendingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MachinesController(AppDbContext databaseContext, IConfiguration configuration) : BaseController(
    databaseContext,
    configuration)
{
    [HttpGet]
    public async Task<IActionResult> ListMachines([FromBody] GetMachinesRequestDto dto)
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (idClaim == null) return Unauthorized("Invalid token");

        var user = await databaseContext.Users.FindAsync(idClaim);
        if (user == null) return Unauthorized("Invalid token");

        var takeMachines = dto.Show ?? 10;
        var page = dto.Page ?? 1;

        var machinesQuery = databaseContext.Machines.Take(takeMachines);

        if (page > 0) machinesQuery = machinesQuery.Skip(takeMachines * (page - 1));
        if (!string.IsNullOrWhiteSpace(dto.Filter))
        {
            machinesQuery = machinesQuery.Where(m => EF.Functions.ILike(m.Model, $"%{dto.Filter}%"));
        }

        var machines = await machinesQuery.ToListAsync();

        return Ok(machines.Select(m => new MachineDto(m.Id, m.Location, m.Model, m.PaymentType, m.Price, m.SerialNumber,
            m.InventoryNumber, m.Manufacter, m.EntryDate.ToUnixTimeMilliseconds(),
            m.ManufactureDate.ToUnixTimeMilliseconds(), m.StartDate.ToUnixTimeMilliseconds(),
            m.LastInspectionDate.ToUnixTimeMilliseconds(), m.NextMaintenanceDate.ToUnixTimeMilliseconds(),
            m.InspectionIntervalMonths, m.ResourceHours, m.MaintenanceHours, m.Status, m.OriginCounty,
            m.LastInspectedById)));
    }
}
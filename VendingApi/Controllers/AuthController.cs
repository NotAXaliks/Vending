using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendingApi.Context;
using VendingApi.Dtos;
using VendingApi.Models;
using Users = VendingApi.Models.Users;

namespace VendingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    public AuthController(AppDbContext databaseContext, IConfiguration configuration) : base(databaseContext,
        configuration)
    {
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] AuthRegisterRequestDto dto)
    {
        Console.WriteLine(JsonSerializer.Serialize(dto));
        /* Валидация */
        if (!ModelState.IsValid) return SendError("Invalid data");
        if (string.IsNullOrWhiteSpace(dto.Email) && string.IsNullOrWhiteSpace(dto.Phone))
            return SendError("Invalid data");
        /* Конец валидации */

        if (!string.IsNullOrWhiteSpace(dto.Phone) && await DatabaseContext.Users.AnyAsync(u => u.Phone == dto.Phone))
            return SendError("User already exists");
        if (await DatabaseContext.Users.AnyAsync(u => u.Email == dto.Email))
            return SendError("User already exists");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new Users
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            Email = dto.Email,
            Phone = dto.Phone,
            Password = hashedPassword,
        };

        await DatabaseContext.Users.AddAsync(user);
        await DatabaseContext.SaveChangesAsync();

        return Ok(UserMeDto.FromDatabaseUser(user));
    }
}
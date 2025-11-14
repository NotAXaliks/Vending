using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VendingApi.Context;
using VendingApi.Dtos;
using VendingApi.Models;
using Users = VendingApi.Models.Users;

namespace VendingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(AppDbContext databaseContext, IConfiguration configuration) : BaseController(
    databaseContext,
    configuration)
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRegisterRequestDto dto)
    {
        /* Валидация */
        if (HttpContext.Connection.RemoteIpAddress == null) return SendError("Invalid data");
        if (!ModelState.IsValid) return SendError("Invalid data");
        /* Конец валидации */

        if (!string.IsNullOrWhiteSpace(dto.Phone))
        {
            if (await DatabaseContext.Users.AnyAsync(u => u.Email == dto.Email || u.Phone == dto.Phone))
                return SendError("User already exists");
        }
        else
        {
            if (await DatabaseContext.Users.AnyAsync(u => u.Email == dto.Email))
                return SendError("User already exists");
        }

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

        var token = GenerateJwtToken(user.Id);
        var refreshToken = GenerateRefreshToken();

        await CreateSessionInDatabase(refreshToken, user.Id, HttpContext, dto.DeviceName);
        await DatabaseContext.SaveChangesAsync();

        return Ok(new AuthAuthResponseDto(UserMeDto.FromDatabaseUser(user), token, refreshToken));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthLoginRequestDto dto)
    {
        /* Валидация */
        if (HttpContext.Connection.RemoteIpAddress == null) return SendError("Invalid data");
        if (!ModelState.IsValid) return SendError("Invalid data");
        /* Конец валидации */

        var user = await DatabaseContext.Users.Where(u => u.Email == dto.Email).FirstOrDefaultAsync();

        if (user == null) return SendError("User not found");
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password)) return SendError("Invalid password");

        var token = GenerateJwtToken(user.Id);
        var refreshToken = GenerateRefreshToken();

        await CreateSessionInDatabase(refreshToken, user.Id, HttpContext, dto.DeviceName);
        await DatabaseContext.SaveChangesAsync();

        return Ok(new AuthAuthResponseDto(UserMeDto.FromDatabaseUser(user), token, refreshToken));
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] AuthRefreshAccessTokenRequestDto dto)
    {
        /* Валидация */
        if (HttpContext.Connection.RemoteIpAddress == null) return SendError("Invalid data");
        if (!ModelState.IsValid) return SendError("Invalid data");
        /* Конец валидации */

        // Проверяем, если токен истек или нет
        var oldSession = await DatabaseContext.Sessions.Where(t => t.RefreshToken == dto.RefreshToken)
            .FirstOrDefaultAsync();
        if (oldSession == null) return SendError("Refresh token not found");
        if (!oldSession.IsActive) return SendError("Refresh token expired");

        oldSession.RevokedAt = DateTimeOffset.UtcNow;

        var token = GenerateJwtToken(oldSession.UserId);
        var refreshToken = GenerateRefreshToken();

        await CreateSessionInDatabase(refreshToken, oldSession.UserId, HttpContext, oldSession.DeviceName);
        await DatabaseContext.SaveChangesAsync();

        return Ok(new AuthRefreshAccessTokenResponseDto(token, refreshToken));
    }

    [HttpGet("checkJWT")]
    [Authorize]
    public async Task<IActionResult> CheckJWT()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (idClaim == null) return Unauthorized("Invalid token");

        var userId = idClaim.Value;

        return Ok($"User id: {userId}");
    }

    private async Task<bool> CreateSessionInDatabase(string token, int userId, HttpContext httpContext,
        string? deviceName)
    {
        var userAgent = httpContext.Request.Headers.UserAgent.ToString();

        await DatabaseContext.Sessions.AddAsync(new Sessions
        {
            RefreshToken = token,
            UserId = userId,
            DeviceName = deviceName,
            IP = httpContext.Connection.RemoteIpAddress?.ToString(),
            UserAgent = string.IsNullOrWhiteSpace(userAgent) ? null : userAgent,
            ExpiresAt = DateTimeOffset.UtcNow.AddDays(Convert.ToDouble(Configuration["Jwt:RefreshTokenExpiresInDays"])),
        });

        return true;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private string GenerateJwtToken(int userId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()), // userId
        };
        var expiresAt = DateTimeOffset.UtcNow.AddMinutes(Convert.ToDouble(Configuration["Jwt:ExpiresInMinutes"]));

        var token = new JwtSecurityToken(issuer: Configuration["Jwt:Issuer"], audience: Configuration["Jwt:Audience"],
            claims: claims ?? [],
            expires: expiresAt.DateTime,
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
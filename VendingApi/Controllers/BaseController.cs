using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VendingApi.Context;
using VendingApi.Dtos;
using VendingApi.Models;

namespace VendingApi.Controllers;

[ApiController]
public abstract class BaseController(AppDbContext databaseContext, IConfiguration configuration) : ControllerBase
{
    protected readonly AppDbContext DatabaseContext = databaseContext;

    protected IActionResult SendError(string error)
    {
        var response = ApiResponse<object>.FromError(error);
        return Ok(response);
    }

    protected IActionResult SendData<T>(T data)
    {
        var response = ApiResponse<T>.FromData(data);
        return Ok(response);
    }

    protected string GenerateJwtToken(Users user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // userId
        };
        var token = new JwtSecurityToken(issuer: configuration["Jwt:Issuer"], audience: configuration["Jwt:Audience"],
            claims: claims ?? [],
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Jwt:ExpiresInMinutes"])),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
using System.ComponentModel.DataAnnotations;

namespace VendingApi.Dtos;

// Передаваемые данные

public record AuthAuthResponseDto(UserMeDto User, string AccessToken, string RefreshToken);

public record AuthRefreshAccessTokenResponseDto(string  AccessToken, string RefreshToken);

// Получаемые данные
public class AuthRegisterRequestDto
{
    [EmailAddress]
    public required string Email { get; set; }

    [Phone]
    public string? Phone { get; set; }
    
    [Required]
    public required string Password { get; set; }
    
    [Required]
    [MinLength(1)]
    [MaxLength(50)]
    public required string LastName { get; set; }
    
    [Required]
    [MinLength(1)]
    [MaxLength(50)]
    public required string FirstName { get; set; }
    
    [MaxLength(50)]
    public string? MiddleName { get; set; }
    
    public string? DeviceName { get; set; }
}

public class AuthLoginRequestDto
{
    [EmailAddress]
    public required string Email { get; set; }
    
    [Required]
    public required string Password { get; set; }
    
    public string? DeviceName { get; set; }
}

public class AuthRefreshAccessTokenRequestDto
{
    [Required]
    public required string RefreshToken { get; set; }
}

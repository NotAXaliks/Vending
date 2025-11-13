using System.ComponentModel.DataAnnotations;
using VendingApi.Models;

namespace VendingApi.Dtos;

// Получаемые данные
public class AuthRegisterRequestDto
{
    [EmailAddress]
    public string? Email { get; set; }

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
}

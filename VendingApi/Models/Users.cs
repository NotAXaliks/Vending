using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VendingApi.Models;

// Готово
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Phone), IsUnique = true)]
public class Users
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
    public int Id { get; set; }

    [MinLength(1)] [MaxLength(50)] public required string LastName { get; set; }

    [MinLength(1)] [MaxLength(50)] public required string FirstName { get; set; }

    [MaxLength(50)] public required string? MiddleName { get; set; }

    [NotMapped] public string FullName => $"{FirstName} {LastName} {MiddleName}".Trim();

    [MinLength(5)] [MaxLength(50)] public required string Email { get; set; }

    [MinLength(6)] [MaxLength(20)] public required string? Phone { get; set; }

    public UserRole Role { get; set; } = UserRole.Default;

    [MinLength(60)]
    [MaxLength(60)] // BCrypt
    public required string Password { get; set; }

    public virtual ICollection<Sessions> Sessions { get; set; } = [];
    public virtual ICollection<Maintenances> Maintenances { get; set; } = [];
    public virtual ICollection<Machines> Inspections { get; set; } = [];
}

public enum UserRole
{
    Admin,
    Operator,
    Default
}
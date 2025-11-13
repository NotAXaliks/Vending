using VendingApi.Models;

namespace VendingApi.Dtos;

public class UserMeDto
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public required UserRole Role { get; set; }
    
    public static UserMeDto FromDatabaseUser(Users user)
    {
        return new UserMeDto
        {
            Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, MiddleName = user.MiddleName,
            Email = user.Email, Phone = user.Phone, Role = user.Role
        };
    }
}

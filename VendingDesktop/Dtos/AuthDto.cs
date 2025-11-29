using System.Text.Json.Serialization;

namespace VendingDesktop.Dtos;

public record UserDto(
    int Id,
    string FirstName,
    string LastName,
    string MiddleName,
    string Email,
    string Phone,
    UserRoleDto Role)
{
    public string FullName => $"{FirstName} {LastName[0]}. {MiddleName[0]}.";

    public string LocalizeRole => Role switch
    {
        UserRoleDto.Admin => "Администратор",
        UserRoleDto.Operator => "Оператор",
        UserRoleDto.Default => "Пользователь",
        _ => "Неизвестная роль"
    };
}

[JsonConverter(typeof(JsonStringEnumConverter<UserRoleDto>))]
public enum UserRoleDto
{
    Admin,
    Operator,
    Default
}

// Получаемые данные

public record LoginResponseDto(UserDto User, string AccessToken, string RefreshToken);

// Передаваемые данные

public record LoginRequestDto(string Email, string Password, string? Device);
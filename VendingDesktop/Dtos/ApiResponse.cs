namespace VendingDesktop.Dtos;

public record ApiResponse<T>(T? Data = default, string? Error = null, bool IsSuccess = false);

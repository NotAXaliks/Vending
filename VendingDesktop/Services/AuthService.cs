using System;
using System.Threading.Tasks;
using VendingDesktop.Dtos;

namespace VendingDesktop.services;

public static class AuthService
{
    public static async Task<ApiResponse<LoginResponseDto>> Login(LoginRequestDto request)
    {
        var loginResponse = await NetService.Post<LoginResponseDto>("/Auth/login", request);

        if (loginResponse.Data == null)
            Console.WriteLine($"Error while fetching POST /Auth/login: {loginResponse.Error}");

        return loginResponse;
    }
}
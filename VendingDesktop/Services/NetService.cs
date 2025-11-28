using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VendingDesktop.Dtos;

namespace VendingDesktop.services;

public static class NetService
{
    private const string ApiBaseUrl = "http://localhost:40004/api";
    private static readonly HttpClient HttpClient = new HttpClient();

    public static async Task<ApiResponse<T>> Get<T>(string path)
    {
        try
        {
            var response = await HttpClient.GetAsync(ApiBaseUrl + path);

            return await GetResponse<T>(response);
        }
        catch (HttpRequestException error)
        {
            return new ApiResponse<T>(default, $"Request Error: {error}");
        }
        catch (Exception error)
        {
            return new ApiResponse<T>(default, $"Unknown Error: {error}");
        }
    }

    public static async Task<ApiResponse<T>> Post<T>(string path, object? data = null)
    {
        try
        {
            var serializedData = JsonSerializer.Serialize(data ?? new { });
            var response = await HttpClient.PostAsync(ApiBaseUrl + path,
                new StringContent(serializedData, Encoding.UTF8, "application/json"));

            return await GetResponse<T>(response);
        }
        catch (HttpRequestException error)
        {
            return new ApiResponse<T>(default, $"Request Error: {error}");
        }
        catch (Exception error)
        {
            return new ApiResponse<T>(default, $"Unknown Error: {error}");
        }
    }

    public static async Task<ApiResponse<T>> Patch<T>(string path, object? data = null)
    {
        try
        {
            var serializedData = JsonSerializer.Serialize(data ?? new { });
            var response = await HttpClient.PatchAsync(ApiBaseUrl + path,
                new StringContent(serializedData, Encoding.UTF8, "application/json"));

            return await GetResponse<T>(response);
        }
        catch (HttpRequestException error)
        {
            return new ApiResponse<T>(default, $"Request Error: {error}");
        }
        catch (Exception error)
        {
            return new ApiResponse<T>(default, $"Unknown Error: {error}");
        }
    }
    
    public static async Task<ApiResponse<T>> Put<T>(string path, object? data = null)
    {
        try
        {
            var serializedData = JsonSerializer.Serialize(data ?? new { });
            var response = await HttpClient.PutAsync(ApiBaseUrl + path,
                new StringContent(serializedData, Encoding.UTF8, "application/json"));

            return await GetResponse<T>(response);
        }
        catch (HttpRequestException error)
        {
            return new ApiResponse<T>(default, $"Request Error: {error}");
        }
        catch (Exception error)
        {
            return new ApiResponse<T>(default, $"Unknown Error: {error}");
        }
    }

    private static async Task<ApiResponse<T>> GetResponse<T>(HttpResponseMessage response)
    {
        var content = string.Empty;
        try
        {
            content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ApiResponse<T>>(content)!;
        }
        catch (JsonException error)
        {
            Console.WriteLine(error);
            return new ApiResponse<T>(default,
                $"Invalid Response: {content.Substring(0, content.Length > 100 ? 100 : content.Length)}");
        }
        catch (Exception error)
        {
            return new ApiResponse<T>(default, $"Unknown error: {error}");
        }
    }
}
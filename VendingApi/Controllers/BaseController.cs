using Microsoft.AspNetCore.Mvc;
using VendingApi.Context;
using VendingApi.Dtos;

namespace VendingApi.Controllers;

[ApiController]
public abstract class BaseController(AppDbContext databaseContext, IConfiguration configuration) : ControllerBase
{
    protected readonly AppDbContext DatabaseContext = databaseContext;
    protected readonly IConfiguration Configuration = configuration;

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
}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RecipeShare.API.Models;
using RecipeShare.Application.Exceptions;
using System.Net;
using System.Text.Json;
namespace RecipeShare.API.Middleware;
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            var response = context.Response;
            response.ContentType = "application/json";
            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Something went wrong.",
                Details = ex.Message
            };
            if (ex is KeyNotFoundException)
            {
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = ex.Message;
            }
            else if (ex is ArgumentNullException)
            {
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = ex.Message;
            }
            response.StatusCode = errorResponse.StatusCode;
            var result = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(result);
        }
    }
}
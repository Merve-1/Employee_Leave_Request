using System.Text.Json;
using LeaveRequests.Api.Services;

namespace LeaveRequests.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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

        catch (EmployeeServiceException e)
        {
            _logger.LogError(e, "Employee service error.");
            await WriteErrorResponseAsync(context, StatusCodes.Status503ServiceUnavailable, e.Message);
        }
        catch (KeyNotFoundException e)
        {
            _logger.LogError(e, "Resource not found.");
            await WriteErrorResponseAsync(context, StatusCodes.Status404NotFound, e.Message);
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, "Invalid Argument");
            await WriteErrorResponseAsync(context, StatusCodes.Status400BadRequest, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled exception");
            await WriteErrorResponseAsync(context, StatusCodes.Status500InternalServerError, "An unexpected error occured");
        }
    }

    private static async Task WriteErrorResponseAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        var response = new
        {
            error = message
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
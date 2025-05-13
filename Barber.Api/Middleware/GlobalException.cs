using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Middleware;

public class GlobalException
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalException> _logger;

    public GlobalException(RequestDelegate next, ILogger<GlobalException> logger)
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
            _logger.LogError(ex, "Unhandled exception occurred");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var problem = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Server Error",
                Detail = "An internal server error has occurred",
                Instance = context.Request.Path
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
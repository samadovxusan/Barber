using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Middleware;

public class StatusCodeMiddleware
{
    private readonly RequestDelegate _next;

    public StatusCodeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.HasStarted)
            return;

        var statusCode = context.Response.StatusCode;
        ProblemDetails? problem = statusCode switch
        {
            400 => new ProblemDetails
            {
                Status = 400,
                Title = "Bad Request",
                Detail = "Invalid request message framing",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            },
            401 => new ProblemDetails
            {
                Status = 401,
                Title = "Unauthorized",
                Detail = "You don’t have access"
            },
            403 => new ProblemDetails
            {
                Status = 403,
                Title = "Forbidden",
                Detail = "Access to the requested resource is denied"
            },
            404 => new ProblemDetails
            {
                Status = 404,
                Title = "Not Found",
                Detail = "The requested resource was not found"
            },
            204 => new ProblemDetails
            {
                Status = 204,
                Title = "No Content",
                Detail = "No Content response"
            },
            _ => null
        };

        if (problem != null)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
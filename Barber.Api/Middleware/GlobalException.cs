using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Middleware;

public class GlobalException
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalException> _logger;

    public GlobalException(RequestDelegate next, ILogger<GlobalException> logger)
    {
        _logger = logger;
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        //500 ServerError
        try
        {
            await _next(context);
        }
        catch (Exception )
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Server Error",
                Detail = "An internal server error has occurred",
               
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
        //204 NoContent
        if (context.Response.StatusCode == (int)HttpStatusCode.NoContent)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NoContent;
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.NoContent,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "No Content",
                Detail = "No Content success status response code indicates",
                Instance = context.Request.Path.Value

            };
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        //404 NotFound
        if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "NotFound",
                Detail = "The requested resource was not found",
                Instance = context.Request.Path.Value
            };
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        //403 Forbidden
        if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.Forbidden,
                Type = "Forbidden",
                Title = "Forbidden",
                Detail = "access to the requested resource is denied",
                Instance = context.Request.Path.Value

            };
            await context.Response.WriteAsJsonAsync(problemDetails);

        }
        //401 Unauthorized
        if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Unauthorized",
                Detail = "You don’t have access",
                Instance = context.Request.Path.Value
            };
            await context.Response.WriteAsJsonAsync(problemDetails);

        }
        //400 BadRequest
        if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest)
        {
            context.Response.StatusCode = (int)(HttpStatusCode.BadRequest);
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "BadRequest",
                Detail = " Ivalid request message framing",
                Instance = context.Request.Path.Value
            };
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
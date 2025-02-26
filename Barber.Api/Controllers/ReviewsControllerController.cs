using Barber.Application.Reviews.Commands;
using Barber.Application.Reviews.Models;
using Barber.Application.Reviews.Queries;
using Barber.Application.Reviews.Services;
using Barber.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController(IReviewService service, IMediator mediator) : ControllerBase
{
    [HttpGet("barber/{barberId}")]
    public async Task<IActionResult> GetReviewsByBarber(Guid barberId)
    {
        
        var result = await service.GetReviewsByBarberAsync(barberId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] CreateReviewCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
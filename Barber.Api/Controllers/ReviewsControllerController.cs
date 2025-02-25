using Barber.Application.Reviews.Models;
using Barber.Application.Reviews.Services;
using Barber.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("barber/{barberId}")]
    public async Task<IActionResult> GetReviewsByBarber(Guid barberId)
    {
        var reviews = await _reviewService.GetReviewsByBarberAsync(barberId);
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] Review reviewDto)
    {
        if (reviewDto.Rating < 1 || reviewDto.Rating > 5)
        {
            return BadRequest("Rating must be between 1 and 5.");
        }

        var createdReview = await _reviewService.AddReviewAsync(reviewDto);
        return CreatedAtAction(nameof(GetReviewsByBarber), new { barberId = createdReview.BarberId }, createdReview);
    }
}
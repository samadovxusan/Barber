using Barber.Application.Reviews.Services;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Reviews.Service;

public class ReviewService(AppDbContext appDbContext,IValidator<Review> validator):IReviewService
{
    public async Task<List<Review>> GetReviewsByBarberAsync(Guid barberId)
    {
        return await appDbContext.Reviews
            .Where(r => r.BarberId == barberId)
            .ToListAsync();
    }

    public async Task<Review> AddReviewAsync(Review reviewDto)
    {
        var validationResult = await validator.ValidateAsync(reviewDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        reviewDto.CreatedTime = DateTimeOffset.UtcNow;
        var result = await appDbContext.Reviews.AddAsync(reviewDto);
        await appDbContext.SaveChangesAsync();
        return reviewDto;


    }
}
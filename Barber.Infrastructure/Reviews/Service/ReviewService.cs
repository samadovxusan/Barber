using Barber.Application.Reviews.Services;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Reviews.Service;

public class ReviewService:IReviewService
{
    public Task<List<Review>> GetReviewsByBarberAsync(Guid barberId)
    {
        throw new NotImplementedException();
    }

    public Task<Review> AddReviewAsync(Review reviewDto)
    {
        throw new NotImplementedException();
    }
}
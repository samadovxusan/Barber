using Barber.Application.Reviews.Models;
using Barber.Domain.Entities;

namespace Barber.Application.Reviews.Services;

public interface IReviewService
{
    Task<List<Review>> GetReviewsByBarberAsync(Guid barberId);
    Task<Review> AddReviewAsync(Review reviewDto);
}
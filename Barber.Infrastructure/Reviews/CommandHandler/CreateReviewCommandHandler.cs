using Barber.Application.Reviews.Commands;
using Barber.Application.Reviews.Services;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Reviews.CommandHandler;

public class CreateReviewCommandHandler(IReviewService service):ICommandHandler<CreateReviewCommand,Review>
{
    public async Task<Review> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var newreview = new Review()
        {
            BarberId = request.BarberId,
            UserId = request.UserId,
            Comment = request.Comment,
            Rating = request.Rating,
            CreatedTime = DateTimeOffset.UtcNow

        };
        var result = await service.AddReviewAsync(newreview);

        return result;
    }
}
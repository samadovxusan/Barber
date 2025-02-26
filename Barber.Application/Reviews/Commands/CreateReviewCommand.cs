using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;

namespace Barber.Application.Reviews.Commands;

public class CreateReviewCommand : ICommand<Review>
{
    public Guid UserId { get; set; }
    public Guid BarberId { get; set; }
    public string Comment { get; set; } = default!;
    public int Rating { get; set; }
}
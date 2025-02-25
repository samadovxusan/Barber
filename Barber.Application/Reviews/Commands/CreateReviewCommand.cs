using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;

namespace Barber.Application.Reviews.Commands;

public class CreateReviewCommand : ICommand<Review>
{
    public int ClientId { get; set; }
    public int BarberId { get; set; }
    public string Comment { get; set; } = default!;
    public int Rating { get; set; }
}
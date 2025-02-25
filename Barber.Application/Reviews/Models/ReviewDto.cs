namespace Barber.Application.Reviews.Models;

public class ReviewDto
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int BarberId { get; set; }
    public string Comment { get; set; } = default!;
    public int Rating { get; set; }
}
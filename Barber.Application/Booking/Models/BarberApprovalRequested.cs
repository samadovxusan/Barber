namespace Barber.Application.Booking.Models;

public class BarberApprovalRequested
{
    public Guid UserId { get; set; }
    public string ServiceId { get; set; } = default!;
    public TimeSpan WorkingTime { get; set; }
    public bool Conformetion { get; set; } = false;
}
namespace Barber.Application.Booking.Models;

public class BarberApprovalRequested
{
    public Guid BookingId { get; set; }
    public bool Conformetion { get; set; } = false;
}
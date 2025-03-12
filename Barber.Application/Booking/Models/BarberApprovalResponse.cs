namespace Barber.Application.Booking.Models;

public class BarberApprovalResponse
{
    public Guid BookingId { get; set; }
    public bool IsApproved { get; set; }
}
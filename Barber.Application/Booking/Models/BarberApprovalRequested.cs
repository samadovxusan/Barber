namespace Barber.Application.Booking.Models;

public class BarberApprovalRequested
{
    public Guid BarberId { get; set; }
    public Guid BookingId { get; set; }
    public TimeSpan AppointmentTime { get; set; }
}
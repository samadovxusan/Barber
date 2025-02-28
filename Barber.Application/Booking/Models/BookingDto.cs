namespace Barber.Application.Booking.Models;

public class BookingDto
{
    public Guid UserId { get; set; }
    public Guid ServiceId { get; set; }
    public DateTime AppointmentTime { get; set; }
}
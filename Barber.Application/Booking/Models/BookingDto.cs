using System.Runtime.InteropServices.JavaScript;

namespace Barber.Application.Booking.Models;

public class BookingDto
{
    public Guid UserId { get; set; }
    public Guid BarberId { get; set; }
    public string ServiceId { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeSpan AppointmentTime { get; set; }
 
}
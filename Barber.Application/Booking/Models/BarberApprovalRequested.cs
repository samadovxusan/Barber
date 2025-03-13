namespace Barber.Application.Booking.Models;

public class BarberApprovalRequested
{
    public Guid BarberId { get; set; }
    public Guid UserId { get; set; }
    public string ServiceId { get; set; } = string.Empty;
    public TimeSpan AppointmentTime { get; set; }
    public bool Conformetion { get; set; } = false;
}
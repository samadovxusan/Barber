namespace Barber.Domain.Entities;

public class BookingData
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BarberId { get; set; }
    public string ServiceId { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeSpan AppointmentTime { get; set; }
}
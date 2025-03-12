using Barber.Domain.Common.Commands;

namespace Barber.Application.Booking.Commonds;

public record BookingCreateCommand : ICommand<bool>
{
    public Guid UserId { get; set; }
    public Guid BarberId { get; set; }
    public string ServiceId { get; set; } = string.Empty;
    public TimeSpan AppointmentTime { get; set; }
}
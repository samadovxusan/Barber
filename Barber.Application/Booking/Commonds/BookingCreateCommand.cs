using Barber.Domain.Common.Commands;

namespace Barber.Application.Booking.Commonds;

public record BookingCreateCommand : ICommand<bool>
{
    public Guid UserId { get; set; }
    public Guid BarberId { get; set; }
    public Guid ServiceId { get; set; }
    public DateTime AppointmentTime { get; set; }
}
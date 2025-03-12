using Barber.Domain.Common.Commands;

namespace Barber.Application.Booking.Commonds;

public record BookingUpdateCommand:ICommand<bool>
{
    public Guid Id { get; set; }
    public TimeSpan AppointmentTime { get; set; }
}
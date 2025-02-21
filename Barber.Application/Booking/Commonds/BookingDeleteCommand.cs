using Barber.Domain.Common.Commands;

namespace Barber.Application.Booking.Commonds;

public record BookingDeleteCommand:ICommand<bool>
{
    public Guid Id { get; set; }
}
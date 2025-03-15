using Barber.Application.Booking.Models;
using Barber.Domain.Common.Commands;

namespace Barber.Application.Booking.Commonds;

public record BookingCreateCommand : ICommand<bool>
{
    public BookingDto BookingDto { get; set; } = new BookingDto();
}
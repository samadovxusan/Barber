using Barber.Application.Booking.Models;
using Barber.Domain.Common.Commands;

namespace Barber.Application.Booking.Commonds;

public record BookingCreateCommand : ICommand<Domain.Entities.Booking>
{
    public BookingDto BookingDto { get; set; } = new BookingDto();
}
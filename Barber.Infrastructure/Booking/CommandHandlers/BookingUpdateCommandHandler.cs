using Barber.Application.Booking.Commonds;
using Barber.Application.Booking.Service;
using Barber.Domain.Common.Commands;

namespace Barber.Infrastructure.Booking.CommandHandlers;

public class BookingUpdateCommandHandler(IBookingService service):ICommandHandler<BookingUpdateCommand,bool>
{
    public async Task<bool> Handle(BookingUpdateCommand request, CancellationToken cancellationToken)
    {
        var booking = await service.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        booking.BookingTime = request.AppointmentTime;
        booking.ModifiedTime = DateTimeOffset.UtcNow;

        var result = await service.UpdateAsync(booking, cancellationToken: cancellationToken);
        return true;
    }
}
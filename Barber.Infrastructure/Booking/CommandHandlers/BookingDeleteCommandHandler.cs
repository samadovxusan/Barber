using Barber.Application.Booking.Commonds;
using Barber.Application.Booking.Service;
using Barber.Domain.Common.Commands;

namespace Barber.Infrastructure.Booking.CommandHandlers;

public class BookingDeleteCommandHandler(IBookingService service):ICommandHandler<BookingDeleteCommand,bool>
{
    public async Task<bool> Handle(BookingDeleteCommand request, CancellationToken cancellationToken)
    {
        var result = await service.DeleteByIdAsync(request.Id, cancellationToken: cancellationToken);
        return true;
    }
}
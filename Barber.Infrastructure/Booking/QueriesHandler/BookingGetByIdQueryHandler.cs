using Barber.Application.Booking.Queries;
using Barber.Application.Booking.Service;
using Barber.Domain.Common.Queries;

namespace Barber.Infrastructure.Booking.QueriesHandler;

public class BookingGetByIdQueryHandler(IBookingService service):IQueryHandler<GetByIdBookingQuery,Domain.Entities.Booking>
{
    public async Task<Domain.Entities.Booking> Handle(GetByIdBookingQuery request, CancellationToken cancellationToken)
    {
        var result = await service.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        return result;
    }
}
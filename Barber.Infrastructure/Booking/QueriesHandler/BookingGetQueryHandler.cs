using Barber.Application.Booking.Queries;
using Barber.Application.Booking.Service;
using Barber.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Booking.QueriesHandler;

public class BookingGetQueryHandler(IBookingService service)
    : IQueryHandler<BookingGetQuery, List<Domain.Entities.Booking>>
{
    public async Task<List<Domain.Entities.Booking>> Handle(BookingGetQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Filters != null)
        {
            var allBookings = await service
                .Get(request.Filters, new QueryOptions() { TrackingMode = QueryTrackingMode.AsNoTracking })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            foreach (var booking in allBookings)
            {
                booking.ServiceIdsArray = string.IsNullOrEmpty(booking.ServiceId)
                    ? Array.Empty<Guid>()
                    : booking.ServiceId.Split(',')
                        .Where(s => Guid.TryParse(s, out _))
                        .Select(Guid.Parse)
                        .ToArray();
            }

            return allBookings;
        }

        return null;
    }
}
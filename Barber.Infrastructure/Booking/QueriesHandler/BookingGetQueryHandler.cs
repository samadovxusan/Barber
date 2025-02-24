using Barber.Application.Booking.Queries;
using Barber.Application.Booking.Service;
using Barber.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Booking.QueriesHandler;

public  class BookingGetQueryHandler(IBookingService service):IQueryHandler<BookingGetQuery,List<Domain.Entities.Booking>>
{
    public async Task<List<Domain.Entities.Booking>> Handle(BookingGetQuery request, CancellationToken cancellationToken)
    {
        if (request.Filters != null)
        {
            var allBookings = await service
                .Get(request.Filters, new QueryOptions() { TrackingMode = QueryTrackingMode.AsNoTracking })
                .Include(b => b.User) 
                .Include(b => b.Barber)   
                .Include(b => b.Service)  
                .ToListAsync(cancellationToken);

            return allBookings;
        }

        return null;
    }

    
}
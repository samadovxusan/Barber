using Barber.Application.Barbers.Queries;
using Barber.Application.Barbers.Services;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Barbers.QueryHandlers;

public class BarberGetAllQueryHandler(IBarberService service) : ICommandHandler<BarberGetAllQuary, List<Domain.Entities.Barber>>
{
    public async Task<List<Domain.Entities.Barber>> Handle(BarberGetAllQuary request, CancellationToken cancellationToken)
    {
        var allBarbers = await service
            .Get(request.FilterPagination, new QueryOptions() { TrackingMode = QueryTrackingMode.AsNoTracking })
            .Include(b => b.Bookings)
            .ToListAsync(cancellationToken);

        foreach (var barber in allBarbers)
        {
            barber.Bookings = barber.Bookings.Select(b => new Domain.Entities.Booking
            {
                UserId = b.UserId,
                ServiceId = b.ServiceId,
                AppointmentTime = b.AppointmentTime
            }).ToList();
        }

        return allBarbers;
    }

}
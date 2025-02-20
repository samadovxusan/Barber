using Barber.Application.Servises.Queries;
using Barber.Application.Servises.Sarvices;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Servises.QueryHandlers;

public class GetAllQueryHandler(IService services):IQueryHandler<ServiceGetQuery,List<Service>>
{
    public async Task<List<Service>> Handle(ServiceGetQuery request, CancellationToken cancellationToken)
    {
        var allServices = await services.Get(
                request.Filters,
                new QueryOptions { TrackingMode = QueryTrackingMode.AsNoTracking } // Tracking rejimi
            ).Include(s => s.Barber) // Barber ma'lumotlarini yuklash
            .ToListAsync(cancellationToken);

        return allServices;
    }

}
using Barber.Application.Barbers.Queries;
using Barber.Application.Barbers.Services;
using Barber.Domain.Common.Queries;

namespace Barber.Infrastructure.Barbers.QueryHandlers;

public class BarberGetByIdQueryHandler(IBarberService service)
    : IQueryHandler<BarberGetByIdQuery, Domain.Entities.Barber>
{
    public async Task<Domain.Entities.Barber> Handle(BarberGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundUBarber = await service.GetByIdAsync(
            request.Id, new QueryOptions(QueryTrackingMode.AsNoTracking), cancellationToken
        );
        
        return foundUBarber;
    }
}
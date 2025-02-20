using Barber.Application.Servises.Queries;
using Barber.Application.Servises.Sarvices;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Servises.QueryHandlers;

public class GetByIdQueryHandler(IService services):IQueryHandler<GetByIdQuery,Service>
{
    public async Task<Service> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundUBarber = await services.GetByIdAsync(
            request.Id,
            new QueryOptions(QueryTrackingMode.AsNoTracking), cancellationToken
        );
        return foundUBarber;
    }
}
using System.Linq.Expressions;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;

namespace Barber.Persistence.Repositories.Interface;

public interface ILocationRepository
{
    IQueryable<Location> Get(Expression<Func<Location, bool>>? predicate = default,
        QueryOptions queryOptions = default);

    ValueTask<Location?> GetById(Guid locationId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Location> Create(Location? location, CancellationToken cancellationToken = default,
        CommandOptions commandOptions = default);

    ValueTask<Location> Update(Location? location, CancellationToken cancellationToken = default,
        CommandOptions commandOptions = default);

    ValueTask<Location?> Delete(Guid locationId, CommandOptions commandOptions,
        CancellationToken cancellationToken = default);

}
using System.Linq.Expressions;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;

namespace Barber.Persistence.Repositories.Interface;

public class LocationRepositories: ILocationRepository
{
    public IQueryable<Location> Get(Expression<Func<Location, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Location?> GetById(Guid locationId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Location> Create(Location? location, CancellationToken cancellationToken = default,
        CommandOptions commandOptions = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Location> Update(Location? location, CancellationToken cancellationToken = default,
        CommandOptions commandOptions = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Location?> Delete(Guid locationId, CommandOptions commandOptions, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
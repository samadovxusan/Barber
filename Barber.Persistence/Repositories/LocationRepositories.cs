using System.Linq.Expressions;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.Caching.Brokers;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Repositories.Interface;

namespace Barber.Persistence.Repositories;

public class LocationRepositories(AppDbContext context,ICacheBroker cacheBroker):EntityRepositoryBase<Location,AppDbContext>(context,cacheBroker),ILocationRepository
{
    public IQueryable<Location> Get(Expression<Func<Location, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public ValueTask<Location?> GetById(Guid locationId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
    {
        return  base.GetByIdAsync(locationId, queryOptions, cancellationToken);
    }

    public ValueTask<Location> Create(Location? location, CancellationToken cancellationToken = default,
        CommandOptions commandOptions = default)
    {
        return base.CreateAsync(location, commandOptions, cancellationToken);
    }

    public ValueTask<Location> Update(Location? location, CancellationToken cancellationToken = default,
        CommandOptions commandOptions = default)
    {
        return base.UpdateAsync(location, commandOptions, cancellationToken);
    }

    public ValueTask<Location?> Delete(Guid locationId, CommandOptions commandOptions, CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(locationId, commandOptions, cancellationToken);
    }

 
}
using System.Linq.Expressions;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;

namespace Barber.Persistence.Repositories.Interface;

public class LocationRepositories(AppDbContext context):EntityRepositoryBase<Location,AppDbContext>(context),ILocationRepository
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
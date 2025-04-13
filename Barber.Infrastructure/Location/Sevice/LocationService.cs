using System.Linq.Expressions;
using Barber.Application.Location.Service;
using Barber.Application.Users.Models;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Extensions;
using Barber.Persistence.Repositories.Interface;

namespace Barber.Infrastructure.Location.Sevice;

public class LocationService(ILocationRepository _repository):ILocationService
{
    public IQueryable<Domain.Entities.Location> Get(Expression<Func<Domain.Entities.Location, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return _repository.Get(predicate, queryOptions);
    }

    public IQueryable<Domain.Entities.Location> Get(UserFilter clientFilter, QueryOptions queryOptions = default)
    {

        return _repository.Get(queryOptions: queryOptions).ApplyPagination(clientFilter);
    }

    public ValueTask<Domain.Entities.Location?> GetByIdAsync(Guid locationId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetById(locationId, queryOptions, cancellationToken);
    }

    public ValueTask<Domain.Entities.Location> CreateAsync(Domain.Entities.Location location, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return _repository.Create(location, cancellationToken);
    }

    public ValueTask<Domain.Entities.Location> UpdateAsync(Domain.Entities.Location location, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return _repository.Update(location, cancellationToken);
    }

}
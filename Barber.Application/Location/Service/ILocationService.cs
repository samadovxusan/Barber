using System.Linq.Expressions;
using Barber.Application.Location.Models;
using Barber.Application.Users.Models;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;

namespace Barber.Application.Location.Service;

public interface ILocationService
{
    IQueryable<Domain.Entities.Location> Get(Expression<Func<Domain.Entities.Location, bool>>? predicate = default,
        QueryOptions queryOptions = default);

    IQueryable<Domain.Entities.Location> Get(UserFilter clientFilter, QueryOptions queryOptions = default);

    IQueryable<Domain.Entities.Barber> Get(Guid barberId, QueryOptions queryOptions = default);

    ValueTask<Domain.Entities.Location?> GetByIdAsync(Guid locationId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Domain.Entities.Location> CreateAsync(LocationDto location, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);


    ValueTask<Domain.Entities.Location> UpdateAsync(LocationDto location, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
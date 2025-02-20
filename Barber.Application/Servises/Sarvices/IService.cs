using System.Linq.Expressions;
using Barber.Application.Servises.Models;
using Barber.Application.Users.Models;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;

namespace Barber.Application.Servises.Sarvices;

public interface IService
{
    IQueryable<Service> Get(Expression<Func<Service, bool>>? predicate = default, QueryOptions queryOptions = default);

    IQueryable<Domain.Entities.Service> Get(ServiceFilters clientFilter, QueryOptions queryOptions = default);

    ValueTask<Service?> GetByIdAsync(Guid userId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Service> CreateAsync(Service service, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);


    ValueTask<Service> UpdateAsync(Service service, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);


    ValueTask<Service?> DeleteByIdAsync(Guid serviceId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
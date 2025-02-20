using System.Linq.Expressions;
using Barber.Application.Servises.Models;
using Barber.Application.Servises.Sarvices;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Extensions;
using Barber.Persistence.Repositories.Interface;

namespace Barber.Infrastructure.Servises.Services;

public class Servicee(IServiceRepository serviceRepository) : IService
{
    public IQueryable<Service> Get(Expression<Func<Service, bool>>? predicate = default,
        QueryOptions queryOptions = default)
    {
        return serviceRepository.Get(predicate, queryOptions);
    }

    public IQueryable<Service> Get(ServiceFilters clientFilter, QueryOptions queryOptions = default)
    {
        return serviceRepository.Get(queryOptions: queryOptions).ApplyPagination(clientFilter);
    }

    public ValueTask<Service?> GetByIdAsync(Guid userId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return serviceRepository.GetByIdAsync(userId, queryOptions, cancellationToken);
    }

    public ValueTask<Service> CreateAsync(Service service, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return serviceRepository.CreateAsync(service, commandOptions, cancellationToken);
    }

    public ValueTask<Service> UpdateAsync(Service service, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return serviceRepository.UpdateAsync(service, commandOptions, cancellationToken);
    }

    public ValueTask<Service?> DeleteByIdAsync(Guid serviceId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return serviceRepository.DeleteByIdAsync(serviceId, commandOptions, cancellationToken);
    }
}
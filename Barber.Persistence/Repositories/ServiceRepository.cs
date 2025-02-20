using System.Linq.Expressions;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Repositories.Interface;

namespace Barber.Persistence.Repositories;

public class ServiceRepository(AppDbContext appContext)
    : EntityRepositoryBase<Service, AppDbContext>(appContext), IServiceRepository
{
    public IQueryable<Service> Get(Expression<Func<Service, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<Service?> GetByIdAsync(Guid clientId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(clientId, queryOptions,cancellationToken);
    }

    public new ValueTask<Service> CreateAsync(Service service, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(service, commandOptions, cancellationToken);
    }

    public new ValueTask<Service> UpdateAsync(Service service, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(service, commandOptions, cancellationToken);

    }

    public new ValueTask<Service?> DeleteByIdAsync(Guid clientId, CommandOptions commandOptions, CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(clientId, commandOptions, cancellationToken);
    }
}
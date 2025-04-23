using System.Linq.Expressions;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Persistence.Caching.Brokers;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Repositories.Interface;

namespace Barber.Persistence.Repositories;

public class BarberRepositories(AppDbContext appDbContext , ICacheBroker cacheBroker):EntityRepositoryBase<Domain.Entities.Barber, AppDbContext>(appDbContext ,cacheBroker),IBarberRepository
{
    public new IQueryable<Domain.Entities.Barber> Get(Expression<Func<Domain.Entities.Barber, bool>>? predicate = default, QueryOptions queryOptions = default)
        => base.Get(predicate, queryOptions);

    public new ValueTask<Domain.Entities.Barber?> GetByIdAsync(Guid clientId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
        => base.GetByIdAsync(clientId, queryOptions,cancellationToken);

    public new ValueTask<Domain.Entities.Barber> CreateAsync(Domain.Entities.Barber user,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => base.CreateAsync(user, commandOptions: commandOptions, cancellationToken: cancellationToken);

    public new ValueTask<Domain.Entities.Barber> UpdateAsync(Domain.Entities.Barber user,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => base.UpdateAsync(user, commandOptions: commandOptions, cancellationToken: cancellationToken);

    public new ValueTask<Domain.Entities.Barber?> DeleteByIdAsync(Guid clientId, CommandOptions commandOptions,
        CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(clientId, commandOptions:commandOptions, cancellationToken: cancellationToken);
}
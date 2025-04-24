using System.Linq.Expressions;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.Caching.Brokers;
using Barber.Persistence.Caching.Models;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Extensions;
using Barber.Persistence.Repositories.Interface;

namespace Barber.Persistence.Repositories;

public class UserRepositories(AppDbContext appContext,ICacheBroker cacheBroker)
    : EntityRepositoryBase<User, AppDbContext>(appContext,cacheBroker, new CacheEntryOptions()), IUserRepository
{
    public new IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<User?> GetByIdAsync(Guid clientId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(clientId, queryOptions, cancellationToken);
    }

    public new ValueTask<User> CreateAsync(User user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var paswordhash = PasswordHelper.HashPassword(user.Password);
        user.Password = paswordhash;
        return base.CreateAsync(user, commandOptions, cancellationToken);
    }

    public new ValueTask<User> UpdateAsync(User user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(user, commandOptions, cancellationToken);
    }

    public new ValueTask<User?> DeleteByIdAsync(Guid clientId, CommandOptions commandOptions,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(clientId, commandOptions, cancellationToken);
    }
}
using System.Linq.Expressions;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;

namespace Barber.Persistence.Repositories.Interface;

public interface IBarberRepository
{
     IQueryable<Domain.Entities.Barber> Get(Expression<Func<Domain.Entities.Barber, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Asynchronously retrieves a client entity by its unique identifier.
    /// </summary>
    /// <param name="clientId">The unique identifier of the client.</param>
    /// <param name="queryOptions">Indicates whether the entity should be queried without tracking changes (default is false).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the client entity, or null if not found.</returns>
    ValueTask<Domain.Entities.Barber?> GetByIdAsync(Guid clientId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a new client entity with the specified options.
    /// </summary>
    /// <param name="user">The client entity to create.</param>
    /// <param name="commandOptions">Options for customizing the command (optional).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the created client entity.</returns>
    ValueTask<Domain.Entities.Barber> CreateAsync(Domain.Entities.Barber user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing client entity.
    /// </summary>
    /// <param name="user">The client entity to update.</param>
    /// <param name="commandOptions">Indicates whether changes should be saved to the underlying data store (default is true).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the updated client entity.</returns>
    ValueTask<Domain.Entities.Barber> UpdateAsync(Domain.Entities.Barber user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes a client entity by its unique identifier.
    /// </summary>
    /// <param name="clientId">The unique identifier of the client to delete.</param>
    /// <param name="commandOptions">Indicates whether changes should be saved to the underlying data store (default is true).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the deleted client entity, or null if not found.</returns>
    ValueTask<Domain.Entities.Barber?> DeleteByIdAsync(Guid clientId, CommandOptions commandOptions,
        CancellationToken cancellationToken = default);
}
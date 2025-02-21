using System.Linq.Expressions;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;

namespace Barber.Persistence.Repositories.Interface;

public interface IBookingRepositoriess
{
    IQueryable<Booking> Get(Expression<Func<Booking, bool>>? predicate = default,
        QueryOptions queryOptions = default);

    ValueTask<Booking> GetById(Guid bookingId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Booking> Create(Booking? booking, CancellationToken cancellationToken = default,
        CommandOptions commandOptions = default);

    ValueTask<Booking> Update(Booking? booking, CancellationToken cancellationToken = default,
        CommandOptions commandOptions = default);

    ValueTask<Booking> Delete(Guid bokingId, CommandOptions commandOptions,
        CancellationToken cancellationToken = default);
}
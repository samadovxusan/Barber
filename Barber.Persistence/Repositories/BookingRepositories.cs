using System.Linq.Expressions;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Repositories.Interface;

namespace Barber.Persistence.Repositories;

public class BookingRepositories(AppDbContext appDbContext):EntityRepositoryBase<Booking,AppDbContext>(appDbContext),IBookingRepositoriess
{
    public new IQueryable<Booking> Get(Expression<Func<Booking, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public ValueTask<Booking> GetById(Guid bookingId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(bookingId, queryOptions, cancellationToken);
    }

    public ValueTask<Booking> Create(Booking booking, CancellationToken cancellationToken = default,
        CommandOptions commandOptions = default)
    {
        return base.CreateAsync(booking, commandOptions, cancellationToken);
    }

    public ValueTask<Booking> Update(Booking booking, CancellationToken cancellationToken = default,
        CommandOptions commandOptions = default)
    {
        return base.UpdateAsync(booking, commandOptions, cancellationToken);
    }

    public ValueTask<Booking> Delete(Guid bokingId, CommandOptions commandOptions, CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(bokingId, commandOptions, cancellationToken);
    }
}
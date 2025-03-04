using System.Linq.Expressions;
using Barber.Application.Booking.Models;
using Barber.Application.Booking.Service;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Persistence.Extensions;
using Barber.Persistence.Repositories.Interface;

namespace Barber.Infrastructure.Booking.Services;

public class BookingService(IBookingRepositoriess repositoriess):IBookingService
{
    public IQueryable<Domain.Entities.Booking> Get(Expression<Func<Domain.Entities.Booking, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return repositoriess.Get(predicate, queryOptions);
    }

    public IQueryable<Domain.Entities.Booking> Get(FilterBooking clientFilter, QueryOptions queryOptions = default)
    {
        return repositoriess.Get(queryOptions: queryOptions).ApplyPagination(clientFilter);
    }

    public ValueTask<Domain.Entities.Booking?> GetByIdAsync(Guid bookingId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return repositoriess.GetById(bookingId, queryOptions, cancellationToken);
    }

    public ValueTask<Domain.Entities.Booking> CreateAsync(Domain.Entities.Booking booking, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        booking.CreatedTime =DateTimeOffset.UtcNow;
        return repositoriess.Create(booking, cancellationToken);
    }

    public ValueTask<Domain.Entities.Booking> UpdateAsync(Domain.Entities.Booking booking, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        booking.ModifiedTime = DateTimeOffset.UtcNow;
        return repositoriess.Update(booking, cancellationToken);
    }

    public ValueTask<Domain.Entities.Booking?> DeleteByIdAsync(Guid serviceId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return repositoriess.Delete(serviceId, commandOptions,cancellationToken);
    }
}
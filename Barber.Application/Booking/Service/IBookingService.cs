using System.Linq.Expressions;
using Barber.Application.Booking.Models;
using Barber.Application.Servises.Models;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;

namespace Barber.Application.Booking.Service;

public interface IBookingService
{
    IQueryable<Domain.Entities.Booking> Get(Expression<Func<Domain.Entities.Booking, bool>>? predicate = default,
        QueryOptions queryOptions = default);

    IQueryable<Domain.Entities.Booking> Get(FilterBooking clientFilter, QueryOptions queryOptions = default);

    ValueTask<Domain.Entities.Booking?> GetByIdAsync(Guid bookingId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Domain.Entities.Booking> CreateAsync(Domain.Entities.Booking booking,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);


    ValueTask<Domain.Entities.Booking> UpdateAsync(Domain.Entities.Booking booking,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);


    ValueTask<Domain.Entities.Booking?> DeleteByIdAsync(Guid serviceId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
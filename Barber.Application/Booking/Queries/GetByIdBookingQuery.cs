using Barber.Domain.Common.Queries;

namespace Barber.Application.Booking.Queries;

public record GetByIdBookingQuery:IQuery<Domain.Entities.Booking>
{
    public Guid Id { get; set; }
}
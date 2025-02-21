using Barber.Application.Booking.Models;
using Barber.Application.Servises.Models;
using Barber.Domain.Common.Queries;

namespace Barber.Application.Booking.Queries;

public class BookingGetQuery : IQuery<List<Domain.Entities.Booking>>
{
    public FilterBooking? Filters { set; get; } = default;
}
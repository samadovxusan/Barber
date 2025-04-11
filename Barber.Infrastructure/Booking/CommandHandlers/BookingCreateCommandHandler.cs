using Barber.Application.Booking.Commonds;
using Barber.Application.Booking.Service;
using Barber.Domain.Common.Commands;
using Barber.Domain.Enums;

namespace Barber.Infrastructure.Booking.CommandHandlers;

public class BookingCreateCommandHandler(IBookingService service) : ICommandHandler<BookingCreateCommand, Domain.Entities.Booking>
{
    public async Task<Domain.Entities.Booking> Handle(BookingCreateCommand request, CancellationToken cancellationToken)
    {
        var newbooking = new Domain.Entities.Booking
        {
            Id = Guid.NewGuid(),
            BarberId = request.BookingDto.BarberId,
            ServiceId = string.Join(",",request.BookingDto.ServiceId),
            UserId = request.BookingDto.UserId,
            Status = Status.Pending,
            AppointmentTime = request.BookingDto.AppointmentTime,
            Date = request.BookingDto.Date,
            CreatedTime = DateTimeOffset.UtcNow,
        };
        var result = await service.CreateAsync(newbooking, cancellationToken: cancellationToken);
        if(result)
            return newbooking;
        return new Domain.Entities.Booking();

    }
}
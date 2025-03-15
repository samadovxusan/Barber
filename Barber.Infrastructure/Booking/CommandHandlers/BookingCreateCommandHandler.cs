using Barber.Application.Booking.Commonds;
using Barber.Application.Booking.Service;
using Barber.Domain.Common.Commands;
using Barber.Domain.Enums;

namespace Barber.Infrastructure.Booking.CommandHandlers;

public class BookingCreateCommandHandler(IBookingService service) : ICommandHandler<BookingCreateCommand, bool>
{
    public async Task<bool> Handle(BookingCreateCommand request, CancellationToken cancellationToken)
    {
        var newbooking = new Domain.Entities.Booking
        {
            BarberId = request.BookingDto.BarberId,
            ServiceId = string.Join(",",request.BookingDto.ServiceId),
            UserId = request.BookingDto.UserId,
            Status = Status.Pending,
            AppointmentTime = request.BookingDto.AppointmentTime,
            CreatedTime = DateTimeOffset.UtcNow,
        };
        var result = await service.CreateAsync(newbooking, cancellationToken: cancellationToken);
        if(result)
            return true;
        return false;
    }
}
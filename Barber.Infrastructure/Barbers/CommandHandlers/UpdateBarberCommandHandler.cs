using Barber.Application.Barbers.Commands;
using Barber.Application.Barbers.Madels;
using Barber.Application.Barbers.Services;
using Barber.Domain.Common.Commands;

namespace Barber.Infrastructure.Barbers.CommandHandlers;

public class UpdateBarberCommandHandler(IBarberService service)
    : ICommandHandler<UpdateBarberCommand, Domain.Entities.Barber>
{
    public async Task<Domain.Entities.Barber> Handle(UpdateBarberCommand request, CancellationToken cancellationToken)
    {
        var barber = new BarberDto()
        {
            Id = request.Id,
            Address = request.Address,
            Age = request.Age,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            Password = request.Password,
        };


        var result = await service.UpdateAsync(barber, cancellationToken: cancellationToken);
        result.ModifiedTime = DateTimeOffset.UtcNow;
        return result;
    }
}
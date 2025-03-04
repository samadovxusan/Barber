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
        var result = await service.UpdateAsync(request.BarberDto, cancellationToken: cancellationToken);
        result.ModifiedTime = DateTimeOffset.UtcNow;
        return result;
    }
}
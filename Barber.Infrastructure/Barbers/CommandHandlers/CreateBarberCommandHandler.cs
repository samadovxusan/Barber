using Barber.Application.Barbers.Commands;
using Barber.Application.Barbers.Services;
using Barber.Domain.Common.Commands;

namespace Barber.Infrastructure.Barbers.CommandHandlers;

public class CreateBarberCommandHandler(IBarberService barberService):ICommandHandler<CreateBerberCommand,bool>
{
    public async Task<bool> Handle(CreateBerberCommand request, CancellationToken cancellationToken)
    {
        var result = await barberService.CreateAsync(request.BarberCreate, cancellationToken: cancellationToken);
        return true;
    }
}
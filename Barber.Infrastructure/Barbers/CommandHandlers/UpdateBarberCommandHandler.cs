using Barber.Application.Barbers.Commands;
using Barber.Application.Barbers.Services;
using Barber.Domain.Common.Commands;

namespace Barber.Infrastructure.Barbers.CommandHandlers;

public record UpdateBarberCommandHandler(IBarberService Service):ICommandHandler<UpdateBarberCommand,Domain.Entities.Barber>
{
    public async Task<Domain.Entities.Barber> Handle(UpdateBarberCommand request, CancellationToken cancellationToken)
    {
        
        var result = await Service.UpdateAsync(request.BarberDto, cancellationToken: cancellationToken);
        return result;
    }
}
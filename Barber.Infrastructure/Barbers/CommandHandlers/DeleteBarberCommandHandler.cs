using Barber.Application.Barbers.Commands;
using Barber.Application.Barbers.Services;
using Barber.Domain.Common.Commands;

namespace Barber.Infrastructure.Barbers.CommandHandlers;

public class DeleteBarberCommandHandler(IBarberService service):ICommandHandler<DeleteBarberCommand,bool>
{
    public async Task<bool> Handle(DeleteBarberCommand request, CancellationToken cancellationToken)
    {
        var deletebarber = await service.DeleteByIdAsync(request.Id, cancellationToken: cancellationToken);
        return true;
    }
}
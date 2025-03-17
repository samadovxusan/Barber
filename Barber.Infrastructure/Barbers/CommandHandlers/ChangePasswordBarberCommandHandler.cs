using Barber.Application.Barbers.Commands;
using Barber.Application.Barbers.Services;
using Barber.Domain.Common.Commands;

namespace Barber.Infrastructure.Barbers.CommandHandlers;

public class ChangePasswordBarberCommandHandler(IBarberService service):ICommandHandler<ChangePasswordBarberCommand, bool>
{
    public Task<bool> Handle(ChangePasswordBarberCommand request, CancellationToken cancellationToken)
    {
        return service.ChangPasswordAsync(request.ChangPassword, cancellationToken: cancellationToken);
    }
}
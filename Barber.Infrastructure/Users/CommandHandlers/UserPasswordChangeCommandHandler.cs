using Barber.Application.Users.Commands;
using Barber.Application.Users.Services;
using Barber.Domain.Common.Commands;

namespace Barber.Infrastructure.Users.CommandHandlers;

public class UserPasswordChangeCommandHandler(IUserService service):ICommandHandler<UserPasswordChangeCommand, bool>
{
    public async Task<bool> Handle(UserPasswordChangeCommand request, CancellationToken cancellationToken)
    {
        return await service.ChangPasswordAsync(request.User, cancellationToken: cancellationToken);
    }
}
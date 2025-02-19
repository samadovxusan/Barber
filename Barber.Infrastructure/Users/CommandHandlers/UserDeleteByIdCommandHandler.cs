using Barber.Application.Users.Commands;
using Barber.Application.Users.Services;
using Barber.Domain.Common.Commands;

namespace Barber.Infrastructure.Users.CommandHandlers;

public class UserDeleteByIdCommandHandler(IUserService service):ICommandHandler<UserDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(UserDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        await service.DeleteByIdAsync(request.UserId, cancellationToken: cancellationToken);
        return true;
        
    }
}


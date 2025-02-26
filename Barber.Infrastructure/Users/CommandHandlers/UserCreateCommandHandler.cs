using AutoMapper;
using Barber.Application.Users.Commands;
using Barber.Application.Users.Services;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Users.CommandHandlers;

public class UserCreateCommandHandler(IUserService service, IMapper mapper) : ICommandHandler<UserCreateCommand, bool>
{
    private readonly IMapper _mapper = mapper;

    public async Task<bool> Handle(UserCreateCommand? request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return false;
        }

        var mapUser = new User()
        {
            FullName = request.FullName,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            CreatedTime = DateTimeOffset.UtcNow,
            Roles = request.Roles
        };
        var createUser = await service.CreateAsync(mapUser, cancellationToken: cancellationToken);

        return true;
    }
}

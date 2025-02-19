using AutoMapper;
using Barber.Application.Users.Commands;
using Barber.Application.Users.Models;
using Barber.Application.Users.Services;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Users.CommandHandlers;

public class UserUpdateCommandHandler(IUserService service, IMapper mapper)
    : ICommandHandler<UserUpdateCommand, UserDto>
{
    public async Task<UserDto> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<User>(request.UserDto);

        var updateUser = await service.UpdateAsync(entity, cancellationToken: cancellationToken);

        return mapper.Map<UserDto>(updateUser);
    }
}
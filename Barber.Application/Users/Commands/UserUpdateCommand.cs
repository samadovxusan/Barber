using Barber.Application.Users.Models;
using Barber.Domain.Common.Commands;

namespace Barber.Application.Users.Commands;

public record UserUpdateCommand : ICommand<UserDto>
{
    public UserDto UserDto { get; set; }
}
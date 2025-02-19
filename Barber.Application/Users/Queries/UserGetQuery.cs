using Barber.Application.Users.Models;
using Barber.Domain.Common.Queries;

namespace Barber.Application.Users.Queries;

public class UserGetQuery : IQuery<ICollection<UserDto>>
{
    public UserFilter Filter { get; set; }
}
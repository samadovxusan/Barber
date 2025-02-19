using Barber.Application.Users.Models;
using Barber.Domain.Common.Queries;

namespace Barber.Application.Users.Queries;

/// <summary>
/// Represents a command to retrieve a client by their unique identifier.
/// </summary>
public class UserGetByIdQuery : IQuery<UserDto>
{
    public Guid Id { get; set; }
}
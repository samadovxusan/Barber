using Barber.Application.Users.Models;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Enums;

namespace Barber.Application.Users.Commands;

public record UserCreateCommand : ICommand<bool>
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public Role Roles { get; set; }
}

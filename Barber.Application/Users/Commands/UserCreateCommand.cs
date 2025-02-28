using System.Text.Json.Serialization;
using Barber.Application.Users.Models;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;
using Barber.Domain.Enums;

namespace Barber.Application.Users.Commands;

public record UserCreateCommand : ICommand<bool>
{
    public string FullName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    [JsonIgnore]
    public Role Roles { get; set; } = Role.Customer;
}

using System.Text.Json.Serialization;
using Barber.Domain.Enums;

namespace Barber.Application.Users.Models;

public class UserCreate
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    [JsonIgnore]
    public Role Roles { get; set; } = Role.Customer;
}
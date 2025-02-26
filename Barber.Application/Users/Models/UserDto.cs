using Barber.Domain.Enums;

namespace Barber.Application.Users.Models;

/// <summary>
/// Data transfer object (DTO) representing a user.
/// </summary>
public class UserDto
{
    public Guid Id { get; init; }

    public string FullName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public Role Roles { get; set; } = Role.Customer;
    
}
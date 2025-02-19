namespace Barber.Application.Users.Models;

/// <summary>
/// Data transfer object (DTO) representing a user.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the listing.
    /// </summary>
    public Guid Id { get; init; }

    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    
}
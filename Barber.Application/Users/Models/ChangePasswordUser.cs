namespace Barber.Application.Users.Models;

public class ChangePasswordUser
{
    public Guid Id { get; set; }
    public string Password { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
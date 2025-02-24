namespace Barber.Application.Auth.Models;

public class Login
{
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
}
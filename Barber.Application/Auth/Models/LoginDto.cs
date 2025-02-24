namespace Barber.Application.Auth.Models;

public class LoginDto
{
    public string Token { get; set; } = string.Empty;
    public bool Success { get; set; } = true;
}
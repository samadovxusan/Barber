namespace Barber.Application.Auth.Models;

public class LoginDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public bool Success { get; set; } = true;
}
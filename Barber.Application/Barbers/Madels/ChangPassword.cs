namespace Barber.Application.Barbers.Madels;

public class ChangPassword
{
    public Guid Id { get; set; }
    public string Password { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
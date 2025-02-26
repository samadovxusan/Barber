using Barber.Domain.Enums;

namespace Barber.Application.Barbers.Madels;

public class BarberCreate
{
    public string FullName { get; set; } = default!;
    public int Age { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; }
}
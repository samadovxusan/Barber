using Barber.Domain.Enums;

namespace Barber.Application.Barbers.Madels;

public class BarberInfo
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public int Age { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    
}
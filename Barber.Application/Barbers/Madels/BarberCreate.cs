using Barber.Domain.Enums;

namespace Barber.Application.Barbers.Madels;

public class BarberCreate
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;
    
    public Role Roles { get; set; }
}
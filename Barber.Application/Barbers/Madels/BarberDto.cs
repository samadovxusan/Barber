using Barber.Domain.Common.Entities;
using Barber.Domain.Enums;

namespace Barber.Application.Barbers.Madels;

public class BarberDto : AuditableEntity
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;
    
    public Role Roles { get; set; }
}
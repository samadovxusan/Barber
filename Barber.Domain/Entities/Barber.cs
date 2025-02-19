using Barber.Domain.Common.Entities;

namespace Barber.Domain.Entities;

public class Barber : AuditableEntity
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;
    // public List<Service> Services { get; set; }
}
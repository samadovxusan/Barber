using Barber.Domain.Common.Entities;

namespace Barber.Domain.Entities;

public class Service:AuditableEntity
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    public string ImagerUrl { get; set; } = string.Empty; 
    
    public Guid BarberId { get; set; }
    public Barber? Barber { get; set; }
}

using Barber.Domain.Common.Entities;

namespace Barber.Domain.Entities;

public class Review : AuditableEntity
{
    public Guid BarberId { get; set; }
    public Guid UserId { get; set; }
    public string Comment { get; set; } = default!;
    public int Rating { get; set; } // 1 - 5 ball

    public User? User { get; set; }
    public Barber? Barber { get; set; }
}
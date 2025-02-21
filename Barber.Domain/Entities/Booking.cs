using Barber.Domain.Common.Entities;
using Barber.Domain.Enums;

namespace Barber.Domain.Entities;

public class Booking:AuditableEntity
{
    public DateTime BookingTime { get; set; }
    public Status Status { get; set; } = Enums.Status.Pending;
    
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Guid BarberId { get; set; }
    public Barber? Barber { get; set; }
    
    public Guid ServiceId { get; set; }
    public Service? Service { get; set; }
}

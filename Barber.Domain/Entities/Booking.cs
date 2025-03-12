using System.ComponentModel.DataAnnotations.Schema;
using Barber.Domain.Common.Entities;
using Barber.Domain.Enums;

namespace Barber.Domain.Entities;

public class Booking:AuditableEntity
{
    public TimeSpan AppointmentTime { get; set; }
    public Status Status { get; set; } = Enums.Status.Pending;
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid BarberId { get; set; }
    public Barber? Barber { get; set; }
    public string ServiceId { get; set; } = string.Empty;
    [NotMapped]
    public Guid[] ServiceIdsArray { get; set; } = Array.Empty<Guid>();
}

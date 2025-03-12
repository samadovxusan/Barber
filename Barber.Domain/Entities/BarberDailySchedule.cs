using Barber.Domain.Common.Entities;
using Barber.Domain.Enums;

namespace Barber.Domain.Entities;

public class BarberDailySchedule :AuditableEntity
{

    public Guid BarberId { get; set; }
    public Barber? Barber { get; set; }

    public TimeSpan StartTime { get; set; }  // working Date
    public TimeSpan EndTime { get; set; }    // end Date
    public bool IsWorking { get; set; } = true; 
}
namespace Barber.Domain.Entities;

public class BarbarBusyTime
{
    public Guid Id { get; set; }
    public ICollection<string>? BusyTime { get; set; }
}
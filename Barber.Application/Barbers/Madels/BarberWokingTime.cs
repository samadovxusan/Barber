namespace Barber.Application.Barbers.Madels;

public class BarberWokingTime
{
    public Guid BarberId { get; set; }
    public TimeSpan StartTime { get; set; }  // working Date
    public TimeSpan EndTime { get; set; }    // end Date
}
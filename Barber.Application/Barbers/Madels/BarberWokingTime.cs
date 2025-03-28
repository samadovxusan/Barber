namespace Barber.Application.Barbers.Madels;

public class BarberWokingTime
{
    public Guid BarberId { get; set; }
    public DateTime StartTime { get; set; }  // working Date
    public DateTime EndTime { get; set; }    // end Date
}
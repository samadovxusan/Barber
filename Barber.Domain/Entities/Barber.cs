using Barber.Domain.Common.Entities;

namespace Barber.Domain.Entities;

public class Barber : AuditableEntity
{
    public string FullName { get; set; } = default!;
    public int Age { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
    public ICollection<Images>? Images { get; set; } = new List<Images>();
}
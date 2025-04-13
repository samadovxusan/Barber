using System.Text.Json.Serialization;
using Barber.Domain.Common.Entities;
using Barber.Domain.Enums;

namespace Barber.Domain.Entities;

public class Barber : AuditableEntity
{
    public string FullName { get; set; } = default!;
    public int Age { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public string Region { get; set; } = null!;
    public string District { get; set; } = null!;
    public string Address { get; set; } = null!;
    [JsonIgnore]
    public Role Role { get; set; } = Role.Barber;
    public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
    public ICollection<Images> Images { get; set; } = new List<Images>();
    public ICollection<BarberDailySchedule> BarberWorkingTime { get; set; } = new List<BarberDailySchedule>();
}
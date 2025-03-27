using System.Text.Json.Serialization;
using Barber.Domain.Common.Entities;
using Barber.Domain.Enums;

namespace Barber.Domain.Entities;

public class User : AuditableEntity
{
    public string FullName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public Role Roles { get; set; } = Role.Customer;
    [JsonIgnore]
    public ICollection<Barber> Barbers { get; set; } = new List<Barber>();

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
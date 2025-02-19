using Barber.Domain.Common.Entities;
using Barber.Domain.Enums;

namespace Barber.Domain.Entities;

public class User : AuditableEntity
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public Role Roles { get; set; }


    // public List<Booking> Bookings { get; set; }
}
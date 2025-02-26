using Barber.Application.Barbers.Madels;
using Barber.Domain.Common.Commands;
using Barber.Domain.Enums;

namespace Barber.Application.Barbers.Commands;

public class UpdateBarberCommand : ICommand<Domain.Entities.Barber>
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public int Age { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; }
}
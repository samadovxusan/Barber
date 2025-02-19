using Barber.Application.Barbers.Madels;
using Barber.Domain.Common.Commands;

namespace Barber.Application.Barbers.Commands;

public record CreateBerberCommand:ICommand<bool>
{
    public BarberCreate BarberCreate { get; set; } = default!;
}
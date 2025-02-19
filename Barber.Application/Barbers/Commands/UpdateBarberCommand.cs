using Barber.Application.Barbers.Madels;
using Barber.Domain.Common.Commands;

namespace Barber.Application.Barbers.Commands;

public record UpdateBarberCommand:ICommand<BarberDto>, ICommand<Domain.Entities.Barber>
{
    public BarberDto BarberDto { get; set; } = default!;
}
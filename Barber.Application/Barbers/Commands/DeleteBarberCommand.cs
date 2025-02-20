using Barber.Domain.Common.Commands;

namespace Barber.Application.Barbers.Commands;

public record DeleteBarberCommand : ICommand<bool>
{
    public Guid BarberId { get; set; }
}
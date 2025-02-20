using Barber.Application.Servises.Models;
using Barber.Domain.Common.Commands;

namespace Barber.Application.Servises.Common;

public record ServiceUpdateCommand:ICommand<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    public Guid BarberId { get; set; }
}
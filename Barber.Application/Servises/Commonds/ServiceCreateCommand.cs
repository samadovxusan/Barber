using Barber.Application.Servises.Models;
using Barber.Domain.Common.Commands;
using Microsoft.AspNetCore.Http;

namespace Barber.Application.Servises.Commonds;

public record ServiceCreateCommand:ICommand<bool>
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    
    public Guid BarberId { get; set; }
    
}
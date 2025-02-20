using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Entities;

namespace Barber.Application.Servises.Models;

public class ServiceCreate
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
}
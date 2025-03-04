using Microsoft.AspNetCore.Http;

namespace Barber.Application.Servises.Models;

public class ServiceUpdate
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    public IFormFile ImageUrl { get; set; } = default!; 
    
    public Guid BarberId { get; set; }
}
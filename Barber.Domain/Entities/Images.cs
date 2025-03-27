using System.Text.Json.Serialization;

namespace Barber.Domain.Entities;

public class Images
{
    public Guid Id { get; set; } = new Guid();
    public Guid BarberId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImagePath { get; set; } = String.Empty;
    public Barber Barber { get; set; } = default!;
}
using System.Text.Json.Serialization;

namespace Barber.Domain.Entities;

public class Images
{
    public Guid Id { get; set; } = new Guid();
    public Guid BarberId { get; set; }
    public string? ImagePath { get; set; }
}
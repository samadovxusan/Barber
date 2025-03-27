using Microsoft.AspNetCore.Http;

namespace Barber.Application.Images.Models;

public class ImageModel
{
    public Guid BarberId { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
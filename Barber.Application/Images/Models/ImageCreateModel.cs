using Microsoft.AspNetCore.Http;

namespace Barber.Application.Images.Models;

public class ImageCreateModel
{
    public Guid BarberId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile ImageUrl { get; set; } = default!;
    public decimal Price { get; set; }
}
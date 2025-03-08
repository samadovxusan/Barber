namespace Barber.Application.Barbers.Madels;

public class ImageModel
{
    public Guid BarberId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
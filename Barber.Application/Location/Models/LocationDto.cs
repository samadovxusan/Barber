using System.Text.Json.Serialization;

namespace Barber.Application.Location.Models;

public class LocationDto
{
    public Guid BarberId { get; set; }
    public string Region { get; set; } = null!;
    public string District { get; set; } = null!;
    public string Address { get; set; } = null!;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    [JsonIgnore]
    public Domain.Entities.Barber Barber { get; set; } = null!;
}
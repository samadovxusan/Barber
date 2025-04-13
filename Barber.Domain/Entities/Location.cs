using System.Text.Json.Serialization;
using Barber.Domain.Common.Entities;

namespace Barber.Domain.Entities;

public class Location : AuditableEntity
{
    public string Region { get; set; } = null!;
    public string District { get; set; } = null!;
    public string Address { get; set; } = null!;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    [JsonIgnore]
    public Guid BarberId { get; set; }
    [JsonIgnore]
    public Barber Barber { get; set; } = null!;
}
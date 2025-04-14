using System.Text.Json.Serialization;
using Barber.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Barber.Application.Barbers.Madels;

public class BarberCreate
{
    public string FullName { get; set; } = default!;
    public int Age { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public IFormFile ImageUrl { get; set; } = default!;

}
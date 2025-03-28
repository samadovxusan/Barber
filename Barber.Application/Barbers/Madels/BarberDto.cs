using Barber.Domain.Common.Entities;
using Barber.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Barber.Application.Barbers.Madels;

public class BarberDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; } = default!;
    public int Age { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public IFormFile ImagetUrl { get; set; } = default!;
}
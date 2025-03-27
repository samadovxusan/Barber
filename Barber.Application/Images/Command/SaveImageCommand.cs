using System.Windows.Input;
using Barber.Application.Images.Models;
using Barber.Domain.Common.Commands;
using Microsoft.AspNetCore.Http;

namespace Barber.Application.Images.Command;

public class SaveImageCommand:ICommand<bool>
{
    public Guid BarberId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile ImageUrl { get; set; } = default!;
    public decimal Price { get; set; }
}
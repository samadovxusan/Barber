using System.Net.Mime;
using Barber.Application.Images.Command;
using Barber.Application.Images.Models;
using Barber.Application.Images.Service;
using Barber.Domain.Common.Commands;

namespace Barber.Infrastructure.Images.CommandHandler;

public class ImageSaveCommandHandler(IImageService service):ICommandHandler<SaveImageCommand, bool>
{
    public async Task<bool> Handle(SaveImageCommand request, CancellationToken cancellationToken)
    {
        var image = new ImageCreateModel()
        {
            Name = request.Name,
            Description = request.Description,
            BarberId = request.BarberId,
            ImageUrl = request.ImageUrl,
            Price = request.Price
        };
        
        
        var result=await service.SaveImage(image);
        return result;
    }
}
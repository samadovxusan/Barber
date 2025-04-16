using Barber.Application.Images.Models;
using Barber.Application.Images.Service;
using Barber.Infrastructure.Extentions;
using Barber.Persistence.DataContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Images.Service;

public class ImageService(IWebHostEnvironment webHostEnvironment, AppDbContext context): IImageService
{
    public async ValueTask<bool> SaveImage(ImageCreateModel image)
    {
        var extention = new MethodExtention(webHostEnvironment);
        var picturepa = await extention.AddPictureAndGetPath(image.ImageUrl);

        var newImage = new Domain.Entities.Images()
        {
            Id = Guid.NewGuid(),
            ImagePath = picturepa,
            BarberId = image.BarberId,
            Name = image.Name,
            Description = image.Description,
            Price = image.Price,
        };
        await context.Imageses.AddRangeAsync(newImage);
        await context.SaveChangesAsync();
        return true;
    }

    public async ValueTask<ICollection<Domain.Entities.Images>> GetWorkingByBarberId(Guid barberId)
    {
        var result = await context.Imageses.Where(i => i.BarberId == barberId).ToListAsync();
        return result;
    }
}
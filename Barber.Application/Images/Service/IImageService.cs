using Barber.Application.Images.Models;

namespace Barber.Application.Images.Service;

public interface IImageService
{
    ValueTask<bool> SaveImage(ImageCreateModel image);
    ValueTask<ICollection<Domain.Entities.Images>> GetWorkingByBarberId(Guid barberId);
}
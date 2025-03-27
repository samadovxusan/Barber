using Barber.Api.Extentions;
using Barber.Application.Barbers.Madels;
using Barber.Application.Images;
using Barber.Application.Images.Models;
using Barber.Application.Images.Service;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barber.Api.Controllers;

[Controller]
[Route("api/[controller]")]
public class ImagePathController(IImageService service) : ControllerBase
{
    [HttpPost]
    public async ValueTask<bool> SaveImageWorking(ImageCreateModel image)
    {
        var result = await service.SaveImage(image);
        return result;
    }
}
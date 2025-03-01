using Barber.Api.Extentions;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;

[Controller]
[Route("api/[controller]")]
public class ImagePathController(IWebHostEnvironment webHostEnvironment, AppDbContext appDbContext) : ControllerBase
{
    [HttpPost]
    public async ValueTask<IActionResult> ImageUrl(Guid barberId, IFormFile imageurl)
    {
        var extention = new MethodExtention(webHostEnvironment);
        var picturepa = await extention.AddPictureAndGetPath(imageurl);


        var newimage = new Images()
        {
            BarberId = barberId,
            ImagePath = picturepa
        };
        
        
        appDbContext.Imageses.Add(newimage);
        await appDbContext.SaveChangesAsync();

        return Ok(picturepa);
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Barber.Infrastructure.Extentions;

public class MethodExtention
{
    private readonly IWebHostEnvironment _env;
    public MethodExtention(IWebHostEnvironment webHostEnvironment)
    {
        _env = webHostEnvironment;
    }


    public async Task<string> AddPictureAndGetPath(IFormFile file)
    {
        string pathForSaveInComputer = Path.Combine( _env.WebRootPath, "images",  file.FileName);
        string pathForSaveInDatabase =Path.Combine("http://95.47.238.221:3034", "images",  file.FileName);

        using (var stream = File.Create(pathForSaveInComputer))
        {
            await file.CopyToAsync(stream);
        }
        return pathForSaveInDatabase;
    }
}

namespace Barber.Api.Extentions;

public class MethodExtention(IWebHostEnvironment webHostEnvironment)
{
    private readonly IWebHostEnvironment _env = webHostEnvironment;


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

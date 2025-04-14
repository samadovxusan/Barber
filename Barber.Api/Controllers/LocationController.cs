using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;

public class LocationController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
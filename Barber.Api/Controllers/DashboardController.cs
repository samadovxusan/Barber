using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;

public class DashboardController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
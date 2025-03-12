using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;

public class BarberApprovalController_Controller : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
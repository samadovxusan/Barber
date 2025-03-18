using Barber.Application.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DashboardController(IDashboardService service) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get() =>
        Ok(await service.GetAllCount());
}
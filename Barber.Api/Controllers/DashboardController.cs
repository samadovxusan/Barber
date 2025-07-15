using Barber.Application.Dashboard;
using Barber.Application.Dashboard.Service;
using Barber.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class DashboardController(IDashboardService service) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async ValueTask<IActionResult> Get() =>
        Ok(await service.GetAllCount());
}
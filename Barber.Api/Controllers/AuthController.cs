using Barber.Application.Auth.Models;
using Barber.Application.Auth.Services;
using Barber.Application.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(UserCreate register)
    {
        var result = await authService.Register(register);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Login(Login login)
    {
        var result = await authService.Login(login);
        return Ok(result);
    }
}
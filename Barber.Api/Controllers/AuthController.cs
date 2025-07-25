﻿using AutoMapper;
using Barber.Application.Auth.Models;
using Barber.Application.Auth.Services;
using Barber.Application.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController(IAuthService authService, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(UserCreate register)
    {
        var result = await authService.Register(register);
        return Ok(result);
    }

    [HttpPost("Admin")]
    public async Task<IActionResult> Register(AdminCreate register)
    {
        var newuser = mapper.Map<UserCreate>(register);

        var result = await authService.Register(newuser);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Login(Login login)
    {
        var result = await authService.Login(login);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        var result = await authService.RefreshToken(refreshToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> RevokeRefreshToken(Guid userId)
        => Ok(await authService.RevokeRefreshToken(userId));
}
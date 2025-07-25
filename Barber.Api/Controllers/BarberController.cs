﻿using System.Runtime.InteropServices.JavaScript;
using Barber.Application.Barbers.Commands;
using Barber.Application.Barbers.Madels;
using Barber.Application.Barbers.Queries;
using Barber.Application.Barbers.Services;
using Barber.Application.Users.Commands;
using Barber.Domain.Common.Queries;
using Barber.Persistence.DataContexts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barber.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize
public class BarberController(IMediator mediator, IBarberService service, AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] BarberGetAllQuary barberGetQuery,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(barberGetQuery, cancellationToken);
        return Ok(result);
    }

    [HttpGet("soarch")]
    public async Task<IActionResult> Get(string fullname, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(fullname))
            return BadRequest("Full name is required.");
        
        var result = service.Get()
            .Where(x => x.FullName.ToLower().Contains(fullname.Trim().ToLower()));

        
        return Ok(result);
    }


    [HttpGet("BarberInfo")]
    public async Task<IActionResult> Get(Guid barberId,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await service.GetBarberInfoAsync(barberId, cancellationToken: cancellationToken);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Barber Topilmadi");
        }
    }

    [HttpGet("{barberId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid barberId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new BarberGetByIdQuery() { Id = barberId }, cancellationToken);
        return Ok(result);
    }
    

    [HttpPost]
    public async ValueTask<IActionResult> Post([FromForm] CreateBerberCommand? userCreate,
        CancellationToken cancellationToken)
    {
        if (userCreate == null)
        {
            return BadRequest("User model cannot be null.");
        }

        var result = await mediator.Send(userCreate, cancellationToken);
        return result ? Ok(result) : NoContent();
    }

    [HttpPost("CHangePassword")]
    public async ValueTask<IActionResult> Post(ChangePasswordBarberCommand changePasswordBarberCommand)
    {
        var result = await mediator.Send(changePasswordBarberCommand);
        return result ? Ok(result) : Ok(false);
    }

    [HttpPost("CreateWorkingTime")]
    public async ValueTask<IActionResult> Post(CreateBarberWorkingTimeCommand workingTimeCommand)
    {
        var result = await mediator.Send(workingTimeCommand);
        return result ? Ok(result) : NoContent();
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromForm] UpdateBarberCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{barberId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid barberId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteBarberCommand() { BarberId = barberId }, cancellationToken);
        return result ? Ok() : BadRequest();
    }
    
}
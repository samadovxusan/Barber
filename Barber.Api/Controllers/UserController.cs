using Barber.Application.Users.Commands;
using Barber.Application.Users.Models;
using Barber.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] UserGetQuery userGetQuery, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(userGetQuery, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{clientId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid clientId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UserGetByIdQuery() { Id = clientId }, cancellationToken);
        return result is not null ? Ok(result) : NoContent();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Post([FromBody] UserCreateCommand userCreate,
        CancellationToken cancellationToken)
    {
        if (userCreate == null)
        {
            return BadRequest("User model cannot be null.");
        }

        var result = await mediator.Send(userCreate, cancellationToken);
        return result ? Ok(result) : NoContent();
    }


    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] UserUpdateCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{clientId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid clientId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UserDeleteByIdCommand() { UserId = clientId }, cancellationToken);
        return result ? Ok() : BadRequest();
    }
}
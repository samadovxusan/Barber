using Barber.Application.Barbers.Commands;
using Barber.Application.Servises.Commonds;
using Barber.Application.Servises.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;
[Controller]
[Route("api/[controller]")]
public class ServiceController (IMediator mediator): Controller
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ServiceGetQuery serviceGetQuery,
        CancellationToken cancellationToken)
    {
        // var pagination = barberGetQuery.FilterPagination ?? new FilterPagination();
        var result = await mediator.Send(serviceGetQuery, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{serviceId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid clientId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetByIdQuery() { Id = clientId }, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async ValueTask<IActionResult> Post([FromForm] ServiceCreateCommand? serviceCreate,
        CancellationToken cancellationToken)
    {
        if (serviceCreate == null)
        {
            return BadRequest("User model cannot be null.");
        }

        var result = await mediator.Send(serviceCreate, cancellationToken);
        return result ? Ok(result) : NoContent();
    }


    [HttpPut]
    public async ValueTask<IActionResult> Update([FromForm] ServiceUpdateCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{serviceId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid clientId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ServiceDeleteCommand() { Id = clientId }, cancellationToken);
        return result ? Ok() : BadRequest();
    }
}
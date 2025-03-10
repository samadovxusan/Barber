using Barber.Application.Barbers.Commands;
using Barber.Application.Barbers.Services;
using Barber.Application.Servises.Commonds;
using Barber.Application.Servises.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;
[Controller]
[Route("api/[controller]")]
public class ServiceController (IMediator mediator, IBarberService service): Controller
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
    public async ValueTask<IActionResult> GetById([FromRoute]Guid serviceId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetByIdQuery() { Id = serviceId }, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet($"by-barber{{barberId:guid}}")]
    public async ValueTask<IActionResult> Get(Guid barberId)
        => Ok(await service.GetByIdAsync(barberId));
    

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
    public async ValueTask<IActionResult> DeleteById( Guid serviceId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ServiceDeleteCommand() { Id = serviceId }, cancellationToken);
        return result ? Ok() : BadRequest();
    }
}
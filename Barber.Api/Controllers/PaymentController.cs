using Barber.Application.Payments.Click.Command;
using Barber.Application.Payments.Click.Models;
using Barber.Application.Payments.Click.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController(IMediator mediator, IPaymentService service) : ControllerBase
{
    [HttpPost("CreatePaymentUrl")]
    public async ValueTask<IActionResult> Post([FromBody] CreateUrlCommand createUrlCommand)
    {
        var result = await mediator.Send(createUrlCommand);
        return Ok(result);
    }

    [HttpPost("callback")]
    public async ValueTask<IActionResult> Post([FromForm] ClickCallbackDto query)
    {
        var result = await service.HandleCallbackAsync(query);
        return Ok(result);
    }
}
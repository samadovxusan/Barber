using Barber.Api.Hubs;
using Barber.Application.Barbers.Services;
using Barber.Application.Booking.Commonds;
using Barber.Application.Booking.Queries;
using Barber.Application.Booking.Service;
using Barber.Persistence.DataContexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
namespace Barber.Api.Controllers;

[Controller]
[Route("api/[controller]")]
public class BookingController
    : ControllerBase
{
    private readonly IHubContext<BookingHub> _hubContext;
    private readonly IMediator _mediator;
    private readonly IBookingService _service;

    public BookingController(IHubContext<BookingHub> hubContext,IMediator mediator,IBookingService service)
    {
            _hubContext = hubContext;
            _mediator = mediator;
            _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] BookingGetQuery bookingGetQuery,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(bookingGetQuery, cancellationToken);
        return Ok(result);
    }

    [HttpGet($"by-barber{{barberId:guid}}")]
    public async ValueTask<IActionResult> Get(Guid barberId)
        => Ok(await _service.GetByIdBarberAsync(barberId));
    

    [HttpGet("{bookingId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid bookingId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetByIdBookingQuery() { Id = bookingId }, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async ValueTask<IActionResult> Post([FromBody] BookingCreateCommand? userCreate,  
        CancellationToken cancellationToken)  
    {  
        if (userCreate == null)  
        {  
            return BadRequest("User model cannot be null.");  
        }  

        var result = await _mediator.Send(userCreate, cancellationToken);  

        if (result)  
        {  
            string message = $"{userCreate.UserId} {userCreate.AppointmentTime} joy band qildi!";  
        
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message, cancellationToken: cancellationToken);  
        }  

        return result ? Ok(result) : NoContent();  
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] BookingUpdateCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{bookingId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid bookingId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new BookingDeleteCommand() { Id = bookingId }, cancellationToken);
        return result ? Ok() : BadRequest();
    }
}
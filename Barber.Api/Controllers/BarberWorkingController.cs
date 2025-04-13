using Barber.Api.Hubs;
using Barber.Application.Barbers.Queries;
using Barber.Application.Barbers.Services;
using Barber.Application.Booking.Models;
using Barber.Application.Booking.Service;
using Barber.Domain.Entities;
using Barber.Domain.Enums;
using Barber.Persistence.DataContexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Barber.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BarberWorkingController(
    AppDbContext context,
    IHubContext<BookingHub> hubContext,
    IBookingService bookingService,IBarberService service,IMediator mediator) : ControllerBase
{
    [HttpPost("approve")]
    public async Task<IActionResult> ChuckMessageBooking([FromQuery] BarberApprovalRequested request)
    {
        var result = await bookingService.ChangeBooking(request);
        if (result)
        {
            string message =
                $"{request.BookingId}  joy band qilindi!";
            await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            return Ok(result);
        }

        return Ok(false);
    }
    
    [HttpGet("BusyTime")]
    public async ValueTask<IActionResult> Get(Guid barberId, CancellationToken cancellationToken)
    {
        var result = await service.GetBarberBusyTimeByTimeAsync(barberId, cancellationToken: cancellationToken);
        return Ok(result);
    }
    [HttpGet("Time")]
    public async ValueTask<IActionResult> Get([FromQuery] BarberGetWorkingTImeQuery barberGetWorkingTimeQuery)
    {
        var result = await mediator.Send(barberGetWorkingTimeQuery);
        return Ok(result);
        NoContent();
    }

    [HttpGet("BusyDateTime")]
    public async ValueTask<IActionResult> Get(Guid barberId, DateOnly date, CancellationToken cancellationToken)
    {
        var result = await service.GetBarberBusyTimeByDateAsync(barberId, date, cancellationToken);
        return Ok(result);
    }
}
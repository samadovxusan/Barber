using Barber.Api.Hubs;
using Barber.Application.Booking.Models;
using Barber.Application.Booking.Service;
using Barber.Domain.Entities;
using Barber.Domain.Enums;
using Barber.Persistence.DataContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Barber.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BarberApprovalController(
    AppDbContext context,
    IHubContext<BookingHub> hubContext,
    IBookingService bookingService) : ControllerBase
{
    [HttpPost("approve")]
    public async Task<IActionResult> ChuckMessageBooking([FromBody] BarberApprovalRequested request)
    {
        var result = await bookingService.ChangeBooking(request);
        if (result)
        {
            string message =
                $"{request.BookingId}  joy band qilindi!";
            await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            return Ok(result);
        }

        return BadRequest("Joy Band Bomadi");
    }
}
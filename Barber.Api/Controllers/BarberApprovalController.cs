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
    public async Task<IActionResult> ApproveBooking([FromBody] BarberApprovalRequested request)
    {
        try
        {
            if (request.Conformetion)
            {
                var booking = new Booking
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    BarberId = request.BarberId,
                    ServiceId = request.ServiceId,
                    Status = Status.Confirmed,
                    CreatedTime = DateTimeOffset.UtcNow
                };
                await context.Bookings.AddAsync(booking);
                await context.SaveChangesAsync();
                string message =
                    $"{booking.UserId} {booking.AppointmentTime} {booking.ServiceId} {booking.BarberId} joy band qilindi!";
                await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
                return Ok(true);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Ok(false);
    }
}
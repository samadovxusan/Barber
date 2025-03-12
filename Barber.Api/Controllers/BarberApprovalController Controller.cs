using Barber.Application.Booking.Models;
using Barber.Domain.Enums;
using Barber.Persistence.DataContexts;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;

[ApiController]
[Route("api/barber")]
public class BarberApprovalController : ControllerBase
{
    private readonly AppDbContext _context;

    public BarberApprovalController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("approve")]
    public async Task<IActionResult> ApproveBooking([FromBody] BarberApprovalRequested request)
    {
        var booking = await _context.Bookings.FindAsync(request.BookingId);

        if (booking == null)
            return NotFound("Booking topilmadi");

        booking.Status = Status.Confirmed;
        await _context.SaveChangesAsync();

        return Ok(new { IsApproved = true });
    }

    [HttpPost("reject")]
    public async Task<IActionResult> RejectBooking([FromBody] BarberApprovalRequested request)
    {
        var booking = await _context.Bookings.FindAsync(request.BookingId);

        if (booking == null)
            return NotFound("Booking topilmadi");

        booking.Status = Status.Confirmed;
        await _context.SaveChangesAsync();

        return Ok(new { IsApproved = false });
    }
}
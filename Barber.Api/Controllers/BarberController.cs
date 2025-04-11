using System.Runtime.InteropServices.JavaScript;
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
public class BarberController(IMediator mediator , IBarberService service , AppDbContext context ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] BarberGetAllQuary barberGetQuery,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(barberGetQuery, cancellationToken);
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

    [HttpGet("BarberWorkingTime")]
    public async ValueTask<IActionResult> Get([FromQuery]BarberGetWorkingTImeQuery barberGetWorkingTimeQuery)
    {
        var result = await mediator.Send(barberGetWorkingTimeQuery);
        return Ok(result);NoContent();
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
        return result ? Ok(result): Ok(false);
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


    [HttpGet("barberBusyTime")]
    public async ValueTask<IActionResult> GetBarberBusyTime(Guid barberId, CancellationToken cancellationToken)
    {
        var result = await context.Bookings
            .Where(b => b.BarberId == barberId && b.Confirmed == true)
            .Select(b => new 
            {
                b.Date,  // DateOnly
                b.AppointmentTime, // TimeSpan
                b.ServiceId // Vergul bilan ajratilgan string
            })
            .ToListAsync(cancellationToken);

// Dictionary yaratamiz: Date -> List<{ AppointmentTime, EndTime }>
        var dictionary = new Dictionary<DateOnly, List<object>>();

        foreach (var booking in result)
        {
            // ServiceId stringni vergulga ajratamiz
            var serviceIdsArray = booking.ServiceId.Split(',', StringSplitOptions.RemoveEmptyEntries);

            TimeSpan totalDuration = TimeSpan.Zero;

            foreach (var serviceId in serviceIdsArray)
            {
                var serviceDuration = context.Services
                    .Where(s => s.Id.ToString() == serviceId.Trim())
                    .Select(s => s.Duration)
                    .FirstOrDefault();  

                totalDuration += serviceDuration; // Barcha durationlarni qo‘shamiz
            }

            // EndTime hisoblash
            var endTime = booking.AppointmentTime + totalDuration;

            // Datega asoslanib dictionaryga qo'shamiz
            if (!dictionary.ContainsKey(booking.Date))
            {
                dictionary[booking.Date] = new List<object>();
            }

            // Har bir booking uchun AppointmentTime va EndTime qo‘shiladi
            dictionary[booking.Date].Add(new 
            {
                AppointmentTime = booking.AppointmentTime.ToString(@"hh\:mm\:ss"),
                EndTime = endTime.ToString(@"hh\:mm\:ss")
            });
        }

        return Ok(dictionary);


        

       
    }


}
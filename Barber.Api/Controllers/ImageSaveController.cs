using Barber.Api.Extentions;
using Barber.Application.Barbers.Madels;
using Barber.Application.Images;
using Barber.Application.Images.Command;
using Barber.Application.Images.Models;
using Barber.Application.Images.Service;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barber.Api.Controllers;

[Controller]
[Route("api/[controller]")]
public class ImageSaveController( IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async ValueTask<bool> SaveImageWorking([FromForm] SaveImageCommand command)
    {
        var result = await mediator.Send(command);
        return result;
    }
  
}
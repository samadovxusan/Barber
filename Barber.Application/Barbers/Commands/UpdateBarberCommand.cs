using Barber.Application.Barbers.Madels;
using Barber.Domain.Common.Commands;
using Barber.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Barber.Application.Barbers.Commands;

public class UpdateBarberCommand : ICommand<Domain.Entities.Barber>
{
    public UpdateBarberCommand(BarberDto barberDto)
    {
        BarberDto = barberDto;
    }

    public BarberDto BarberDto { get; set; }
}
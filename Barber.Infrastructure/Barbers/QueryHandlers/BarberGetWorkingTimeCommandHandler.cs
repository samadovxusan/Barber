using Barber.Application.Barbers.Madels;
using Barber.Application.Barbers.Queries;
using Barber.Application.Barbers.Services;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Barbers.QueryHandlers;

public class BarberGetWorkingTimeCommandHandler(IBarberService service):ICommandHandler<BarberGetWorkingTImeQuery, BarberWokingTime>
{
    public async Task<BarberWokingTime> Handle(BarberGetWorkingTImeQuery request, CancellationToken cancellationToken)
    {
        var result = await service.Get(request.BarberId);
        return result;

    }


}
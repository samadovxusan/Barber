using AutoMapper;
using Barber.Application.Barbers.Commands;
using Barber.Application.Barbers.Madels;
using Barber.Application.Barbers.Services;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Barbers.CommandHandlers;

public class CreateBarberWorkingTImeCommandHandler(IBarberService barberService, IMapper mapper)
    : ICommandHandler<CreateBarberWorkingTimeCommand, bool>
{
    public async Task<bool> Handle(CreateBarberWorkingTimeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newbarberTime = mapper.Map<BarberDailySchedule>(request.BarberWokingTime);
            await barberService.SetDailyScheduleAsync(newbarberTime);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);

        }
        return false;
    }
}
using Barber.Application.Barbers.Madels;
using Barber.Domain.Common.Commands;

namespace Barber.Application.Barbers.Commands;

public class CreateBarberWorkingTimeCommand:ICommand<Boolean>
{
    public BarberWokingTime BarberWokingTime { get; set; } = new BarberWokingTime();
}
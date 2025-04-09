using System.Windows.Input;
using Barber.Application.Barbers.Madels;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;

namespace Barber.Application.Barbers.Queries;

public class BarberGetWorkingTImeQuery:ICommand<BarberWokingTime>
{
    public Guid BarberId { get; set; }
}
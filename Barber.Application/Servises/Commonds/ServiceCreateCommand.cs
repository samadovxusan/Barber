using Barber.Application.Servises.Models;
using Barber.Domain.Common.Commands;

namespace Barber.Application.Servises.Commonds;

public class ServiceCreateCommand : ICommand<bool>
{
    public ServiceCreate ServiceCreate { get; set; }
}
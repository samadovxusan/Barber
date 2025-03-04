using Barber.Domain.Common.Commands;

namespace Barber.Application.Servises.Commonds;

public class ServiceDeleteCommand:ICommand<bool>
{
    public Guid Id { get; set; }
}
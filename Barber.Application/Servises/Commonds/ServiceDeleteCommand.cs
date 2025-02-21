using Barber.Domain.Common.Commands;

namespace Barber.Application.Servises.Common;

public class ServiceDeleteCommand:ICommand<bool>
{
    public Guid Id { get; set; }
}
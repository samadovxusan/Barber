using Barber.Application.Servises.Models;
using Barber.Domain.Common.Commands;

namespace Barber.Application.Servises.Commonds;

public record ServiceUpdateCommand:ICommand<bool>
{
  public ServiceUpdateCommand(ServiceUpdate serviceUpdate)
  {
    ServiceUpdate = serviceUpdate;
  }

  public ServiceUpdate ServiceUpdate { get; set; }
}
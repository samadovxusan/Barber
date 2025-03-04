using Barber.Application.Servises.Commonds;
using Barber.Application.Servises.Sarvices;
using Barber.Domain.Common.Commands;

namespace Barber.Infrastructure.Servises.CommonHandlers;

public class ServiceDeleteCommandHandler(IService service):ICommandHandler<ServiceDeleteCommand,bool>
{
    public async Task<bool> Handle(ServiceDeleteCommand request, CancellationToken cancellationToken)
    {
        var result = await service.DeleteByIdAsync(request.Id, cancellationToken: cancellationToken);
        return true;
    }
}
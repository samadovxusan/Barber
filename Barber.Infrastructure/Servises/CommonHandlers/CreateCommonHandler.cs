using AutoMapper;
using Barber.Application.Servises.Commonds;
using Barber.Application.Servises.Sarvices;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Servises.CommonHandlers;

public class CreateCommonHandler(IService serviced,IMapper mapper):ICommandHandler<ServiceCreateCommand,bool >
{
    public async Task<bool> Handle(ServiceCreateCommand request, CancellationToken cancellationToken)
    {
        var newservice = await serviced.CreateAsync(request.ServiceCreate, cancellationToken: cancellationToken);
        return true;
    }
}
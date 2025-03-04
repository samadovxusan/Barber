using AutoMapper;
using Barber.Application.Servises.Commonds;
using Barber.Application.Servises.Sarvices;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Servises.CommonHandlers;

public class UpdateCommandHandler(IService services, IMapper mapper) : ICommandHandler<ServiceUpdateCommand, bool>
{
    public async Task<bool> Handle(ServiceUpdateCommand request, CancellationToken cancellationToken)
    {
        var result = await services.UpdateAsync(request.ServiceUpdate, cancellationToken: cancellationToken);
        return true;
    }
}
using AutoMapper;
using Barber.Application.Servises.Commonds;
using Barber.Application.Servises.Models;
using Barber.Application.Servises.Sarvices;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Servises.CommonHandlers;

public class CreateCommonHandler(IService serviced,IMapper mapper):ICommandHandler<ServiceCreateCommand,bool >
{
    public async Task<bool> Handle(ServiceCreateCommand request, CancellationToken cancellationToken)
    {
        var service = new ServiceCreate
        {
            BarberId = request.BarberId,
            Duration = request.Duration,
            ImageUrl = request.ImageUrl,
            Name = request.Name,
            Price = request.Price
        };
        var newservice = await serviced.CreateAsync(service, cancellationToken: cancellationToken);
        return true;
    }
}
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
        var mapservice = new ServiceCreate()
        {
            Name = request.Name,
            Duration = request.Duration,
            BarberId = request.BarberId,
            Price = request.Price,
            ImageUrl = request.ImageUrl
        };
        var newservice = await serviced.CreateAsync(mapservice, cancellationToken: cancellationToken);
        return true;
    }
}
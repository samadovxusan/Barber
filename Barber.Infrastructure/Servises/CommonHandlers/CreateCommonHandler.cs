using AutoMapper;
using Barber.Application.Servises.Common;
using Barber.Application.Servises.Sarvices;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Servises.CommonHandlers;

public class CreateCommonHandler(IService serviced,IMapper mapper):ICommandHandler<ServiceCreateCommand,bool >
{
    public async Task<bool> Handle(ServiceCreateCommand request, CancellationToken cancellationToken)
    {
        var mapservice = new Service()
        {
            Name = request.Name,
            Duration = request.Duration,
            BarberId = request.BarberId,
            Price = request.Price,
            CreatedTime = DateTimeOffset.UtcNow
        };
        var newservice = await serviced.CreateAsync(mapservice, cancellationToken: cancellationToken);
        return true;
    }
}
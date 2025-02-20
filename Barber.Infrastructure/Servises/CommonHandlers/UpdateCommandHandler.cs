using AutoMapper;
using Barber.Application.Servises.Common;
using Barber.Application.Servises.Sarvices;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Servises.CommonHandlers;

public class UpdateCommandHandler(IService services, IMapper mapper) : ICommandHandler<ServiceUpdateCommand, bool>
{
    public async Task<bool> Handle(ServiceUpdateCommand request, CancellationToken cancellationToken)
    {
        var mapservice = new Service
        {
            Name = request.Name,
            Duration = request.Duration,
            Price = request.Price,
            BarberId = request.BarberId,
            ModifiedTime = DateTimeOffset.UtcNow
        };
        var result = await services.UpdateAsync(mapservice, cancellationToken: cancellationToken);
        return true;
    }
}
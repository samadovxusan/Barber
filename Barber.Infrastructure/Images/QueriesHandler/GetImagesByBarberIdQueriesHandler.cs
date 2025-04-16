using System.Net.Mime;
using Barber.Application.Images.Queries;
using Barber.Application.Images.Service;
using Barber.Domain.Common.Queries;

namespace Barber.Infrastructure.Images.QueriesHandler;

public class GetImagesByBarberIdQueriesHandler(IImageService service):IQueryHandler<GetImagesByBarberIdQueries,ICollection<Domain.Entities.Images>>
{
    public async Task<ICollection<Domain.Entities.Images>> Handle(GetImagesByBarberIdQueries request, CancellationToken cancellationToken)
    {
        var result =  await service.GetWorkingByBarberId(request.BarberId);
        return result;
    }
}
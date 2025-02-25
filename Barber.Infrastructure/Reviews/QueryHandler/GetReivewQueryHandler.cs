using Barber.Application.Reviews.Queries;
using Barber.Application.Reviews.Services;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Reviews.QueryHandler;

public class GetReivewQueryHandler(IReviewService service):IQueryHandler<GetReivewQuery,List<Review>>
{
    public Task<List<Review>> Handle(GetReivewQuery request, CancellationToken cancellationToken)
    {
        return service.GetReviewsByBarberAsync(request.Id);
    }
}
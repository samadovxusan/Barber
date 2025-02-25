using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;

namespace Barber.Application.Reviews.Queries;

public class GetReivewQuery:IQuery<List<Review>>
{
    public Guid Id { get; set; }
}
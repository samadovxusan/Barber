using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;

namespace Barber.Application.Servises.Queries;

public class GetByIdQuery:IQuery<Service>
{
    public Guid Id { get; set; }
}
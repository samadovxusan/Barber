using Barber.Domain.Common.Queries;

namespace Barber.Application.Barbers.Queries;

public record BarberGetByIdQuery:IQuery<Domain.Entities.Barber>
{
    public Guid Id { get; set; }
}
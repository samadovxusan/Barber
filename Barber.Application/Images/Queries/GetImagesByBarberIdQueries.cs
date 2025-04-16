using Barber.Domain.Common.Queries;

namespace Barber.Application.Images.Queries;

public class GetImagesByBarberIdQueries:IQuery<ICollection<Domain.Entities.Images>>
{
    public Guid BarberId { get; set; }
    
}
using Barber.Application.Barbers.Madels;
using Barber.Application.Users.Models;
using Barber.Domain.Common.Commands;
using Barber.Domain.Common.Queries;

namespace Barber.Application.Barbers.Queries;

public record BarberGetAllQuary:IQuery<List<Domain.Entities.Barber>>, ICommand<List<Domain.Entities.Barber>>
{
    public BarberFilter FilterPagination { get; set; } = default!;
}
using AutoMapper;
using Barber.Application.Users.Models;
using Barber.Application.Users.Queries;
using Barber.Application.Users.Services;
using Barber.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Users.QueryHandlers;

public class UserGetQueryHandler(IUserService service, IMapper mapper):IQueryHandler<UserGetQuery,ICollection<UserDto>>
{
    public async Task<ICollection<UserDto>> Handle(UserGetQuery request, CancellationToken cancellationToken)
    {
        var matchedUsers = await service
                               .Get(request.Filter, new QueryOptions() { TrackingMode = QueryTrackingMode.AsNoTracking })
                               .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<UserDto>>(matchedUsers);
    }
}
using AutoMapper;
using Barber.Application.Users.Models;
using Barber.Application.Users.Queries;
using Barber.Application.Users.Services;
using Barber.Domain.Common.Queries;

namespace Barber.Infrastructure.Users.QueryHandlers;

public class UserGetByIdQueryHandler(IUserService service,IMapper mapper):IQueryHandler<UserGetByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundUser = await service.GetByIdAsync(
            request.Id, new QueryOptions(QueryTrackingMode.AsNoTracking), cancellationToken
        );
        return mapper.Map<UserDto>(foundUser);
    }
}
using AutoMapper;
using Barber.Application.Users.Commands;
using Barber.Application.Users.Services;
using Barber.Domain.Common.Commands;
using Barber.Domain.Entities;

namespace Barber.Infrastructure.Users.CommandHandlers;

public class UserCreateCommandHandler : ICommandHandler<UserCreateCommand, bool>
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public UserCreateCommandHandler(IUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            // Requestdagi UserCreate null bo'lsa, xato qaytarish
            return false;
        }

        var mapUser = new User()
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = request.PasswordHash,
            PhoneNumber = request.PhoneNumber,
            CreatedTime = DateTimeOffset.UtcNow,
        };
        var createUser = await _service.CreateAsync(mapUser);

        return true;
    }
}

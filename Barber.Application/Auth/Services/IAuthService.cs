using Barber.Application.Auth.Models;
using Barber.Application.Users.Models;

namespace Barber.Application.Auth.Services;

public interface IAuthService
{
    ValueTask<Boolean> Register (UserCreate register);
    ValueTask<LoginDto> Login (Login login);
}
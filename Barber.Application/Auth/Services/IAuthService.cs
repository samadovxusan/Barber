using Barber.Application.Auth.Models;
using Barber.Application.Users.Models;
using Barber.Domain.Entities;

namespace Barber.Application.Auth.Services;

public interface IAuthService
{
    ValueTask<Boolean> Register (UserCreate register);
    ValueTask<LoginDto> Login (Login login);
    ValueTask<Token> RefreshToken (string refreshToken);
    ValueTask<bool> RevokeRefreshToken (Guid userId);
}
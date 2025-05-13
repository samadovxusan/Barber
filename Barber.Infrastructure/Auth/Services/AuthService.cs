using AutoMapper;
using Barber.Application.Auth.Models;
using Barber.Application.Auth.Services;
using Barber.Application.Users.Models;
using Barber.Application.Users.Services;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Barber.Infrastructure.Auth.Services;

public class AuthService(AppDbContext dbContext, IUserService service, IMapper mapper, IConfiguration configuration)
    : IAuthService
{
    public async ValueTask<bool> Register(UserCreate register)
    {
        try
        {
            var user = mapper.Map<User>(register);
            var newuser = await dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == register.PhoneNumber);
            if (newuser != null)
            {
                throw new InvalidOperationException("This number or name has already been registered.");
            }

            user.Password = PasswordHelper.HashPassword(user.Password);
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async ValueTask<LoginDto> Login(Login login)
    {
        var token = new LoginDto();
        var newUser = await dbContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == login.PhoneNumber);
        if (!PasswordHelper.VerifyPassword(newUser.Password, login.Password))
        {
            token.Success = false;
            return token;
        }

        var jwtToken = new IdentityTokenGeneratorService(configuration, dbContext);
        token.AccessToken = await jwtToken.GenerateToken(newUser);
        var refreshToken = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = newUser.Id,
            Token = jwtToken.GenerateRefreshTokenAsync().Result,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        await dbContext.RefreshTokens.AddAsync(refreshToken);
        await dbContext.SaveChangesAsync();
        token.RefreshToken = refreshToken.Token;
        return token;
    }

    public async ValueTask<Token> RefreshToken(string refreshToken)
    {
        var jwtToken = new IdentityTokenGeneratorService(configuration, dbContext);
        RefreshToken? refreshTokenAsync = await dbContext.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == refreshToken);
        if (refreshToken is null || refreshTokenAsync.Expires < DateTime.UtcNow)
        {
            throw new ApplicationException("Invalid refresh token");
        }
        refreshTokenAsync.Token = await jwtToken.GenerateRefreshTokenAsync();
        refreshTokenAsync.Expires = DateTime.UtcNow.AddDays(7);
        await dbContext.SaveChangesAsync();
        var token = new Token()
        {
            AccessToken = await jwtToken.GenerateToken(refreshTokenAsync.User),
            RefreshToken = refreshTokenAsync.Token,
        };
        return token;
    }

    public async ValueTask<bool> RevokeRefreshToken(Guid userId)
    {
        await dbContext.RefreshTokens
            .Where(r => r.UserId == userId)
            .ExecuteDeleteAsync();
        return true;
    }
}
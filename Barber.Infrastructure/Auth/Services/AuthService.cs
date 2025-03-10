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

public class AuthService(AppDbContext dbContext,IUserService service,  IMapper mapper, IConfiguration configuration): IAuthService
{
    public async  ValueTask<bool> Register(UserCreate register)
    {
        try
        {
            var user = mapper.Map<User>(register);
            var newuser = await dbContext.Users.FirstOrDefaultAsync(u=> u.PhoneNumber== register.PhoneNumber);
            if(newuser != null)
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
        if(!PasswordHelper.VerifyPassword(newUser.Password , login.Password))
        {
            token.Success = false;
            return token;
        }
        var jwtToken = new IdentityTokenGeneratorService(configuration);
        token.Token = await jwtToken.GenerateToken(newUser); 
        return token;
    }
}
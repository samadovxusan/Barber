using AutoMapper;
using Barber.Application.Auth.Models;
using Barber.Application.Auth.Services;
using Barber.Application.Users.Models;
using Barber.Domain.Entities;
using Barber.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Barber.Infrastructure.Auth.Services;

public class AuthService(AppDbContext dbContext, IMapper mapper, IConfiguration configuration): IAuthService
{
    public async  ValueTask<bool> Register(UserCreate register)
    {
        try
        {
            var user = mapper.Map<User>(register);
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
        var newUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Password == login.Password && x.PhoneNumber == login.PhoneNumber);
        if(newUser == null)
        {
            token.Success = false;
            return token;
        }
        var jwtToken = new IdentityTokenGeneratorService(configuration);
        token.Token = await jwtToken.GenerateToken(newUser); 
        return token;
    }
}
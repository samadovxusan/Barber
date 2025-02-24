using System.Security.Claims;
using Barber.Domain.Entities;

namespace Barber.Application.Auth.Services;

public interface IIdentityTokenGeneratorService
{
    Task<string> GenerateToken(User user);
    Task<string> GenerateToken(IEnumerable<Claim> additionalClaims);
    
}
using System.Security.Claims;
using Barber.Application.Auth.Models;
using Barber.Domain.Entities;

namespace Barber.Application.Auth.Services;

public interface IIdentityTokenGeneratorService
{
    Task<string> GenerateToken(User user);
    Task<string> GenerateTokenAsync(IEnumerable<Claim> additionalClaims);
    ValueTask<string> GenerateRefreshTokenAsync();
}
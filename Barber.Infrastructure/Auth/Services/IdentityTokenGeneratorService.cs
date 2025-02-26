using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Barber.Application.Auth.Services;
using Barber.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Barber.Infrastructure.Auth.Services;

public class IdentityTokenGeneratorService(IConfiguration configuration):IIdentityTokenGeneratorService
{
        public async Task<string> GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
                {
                    
                    new Claim("Id", user.Id.ToString()),
                    new Claim("UserName", user.FullName),
                    new Claim("Number", user.PhoneNumber),
                    new Claim("password" , user.Password),
                    new Claim(ClaimTypes.Role , user.Roles.ToString()),
                    new Claim("CreatedDate", DateTime.UtcNow.ToString()),

                };

            return await GenerateToken(claims);
        }
        public async Task<string> GenerateToken(IEnumerable<Claim> additionalClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var exDate = Convert.ToInt32(configuration["JWT:ExpireDate"] ?? "10");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
            };

            if (additionalClaims?.Any() == true)
                claims.AddRange(additionalClaims);

            
            var token = new JwtSecurityToken(
            issuer: configuration["JWT:Issuer"],
            audience: configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(exDate),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
}




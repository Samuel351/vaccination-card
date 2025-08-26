using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Infrastructure.Options;

namespace VaccinationCard.Infrastructure.Services
{
    public class TokenService(IOptions<TokenSettings> options) : ITokenService
    {

        private readonly TokenSettings _tokenSettings = options.Value;

        public string GenerateTokenForUser(User user)
        {
            var claims = new List<Claim>
            {
                new("userId", user.EntityId.ToString()),
                new (ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, "user")
            };

            return WriteToken(_tokenSettings.JwtSecret, claims, _tokenSettings.TokenLifetimeHours, _tokenSettings.Issuer, _tokenSettings.Audience);
        }

        private static string WriteToken(string signingKey, List<Claim> claims, double expirationHours, string issuer, string audience)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(signingKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = audience,
                Issuer = issuer,
                IssuedAt = DateTime.UtcNow, 
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(expirationHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

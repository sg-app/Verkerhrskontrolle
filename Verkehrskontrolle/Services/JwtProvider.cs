using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Verkehrskontrolle.Interfaces;
using Verkehrskontrolle.Models;
using Verkehrskontrolle.Options;

namespace Verkehrskontrolle.Services
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtSettings _jwtSettings;

        public JwtProvider(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(User user)
        {
            var claims = new Claim[] {
                new(JwtRegisteredClaimNames.Email, user.Email),
            };

            var signingCred = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                null,
                DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
                signingCred);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}

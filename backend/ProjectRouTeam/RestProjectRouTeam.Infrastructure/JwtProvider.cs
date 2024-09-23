using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestProjectRouTeam.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestProjectRouTeam.Infrastructure
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(User user)
        {
            Claim[] claims = [new("userId", user.Id.ToString())];

            var signingCreds = new SigningCredentials
            (
                key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                algorithm: SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken
            (
                claims: claims,
                signingCredentials: signingCreds,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
            );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
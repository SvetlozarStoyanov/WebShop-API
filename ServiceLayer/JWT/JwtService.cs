using Contracts.Services.JWT;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Services.JWT
{
    public class JwtService : IJwtService
    {
        private readonly IOptions<JWTConfiguration> options;

        public JwtService(IOptions<JWTConfiguration> options)
        {
            this.options = options;
        }

        public string GenerateJwtToken(string userName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: options.Value.Issuer,
                audience: options.Value.Audience,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

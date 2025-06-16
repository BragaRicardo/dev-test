using DevTest_API.Configuration;
using DevTest_API.Entities;
using DevTest_API.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevTest_API.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;
        public TokenService(IOptions<JwtSettings> opts) => _settings = opts.Value;

        public string GenerarToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Key!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.NombreCompleto),
            new Claim(ClaimTypes.Role, usuario.Rol.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

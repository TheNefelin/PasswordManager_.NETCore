using ClassLibrary.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClassLibrary.Services.Utils
{
    class JwtTokenUtil
    {
        public string GenerateJwtToken(JwtUser user, JwtConfig config)
        {
            // Define los claims (información contenida en el token)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.IdUser),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // Genera una clave simétrica a partir del secret en appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Configuración del token: audiencia, emisor, expiración y firma
            var token = new JwtSecurityToken(
                issuer: config.Issuer,
                audience: config.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(config.ExpireMin)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

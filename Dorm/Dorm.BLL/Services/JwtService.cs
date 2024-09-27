using Dorm.BLL.Settings;
using Dorm.Domain.Entities.UserEF;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dorm.BLL.Services
{
    public class JwtService(IOptions<AuthSettings> options)
    {
        public string GetToken(UserEF user)
        {
            var claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey! ?? throw new Exception("Secret key is null")));
            var token = new JwtSecurityToken(
                expires: DateTime.Now.Add(options.Value.TimeExp),
                claims: claims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        
        public string? GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(options.Value.SecretKey! ?? throw new Exception("Secret key is null"));

                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);

                return principal.FindFirst("id")?.Value;
        }
    }
}

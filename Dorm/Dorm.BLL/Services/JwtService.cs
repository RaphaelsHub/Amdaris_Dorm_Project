using Dorm.BLL.Settings;
using Dorm.Domain.Entities.UserEF;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey));
            var token = new JwtSecurityToken(
                expires: DateTime.Now.Add(options.Value.TimeExp),
                claims: claims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}

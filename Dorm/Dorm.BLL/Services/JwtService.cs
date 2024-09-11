using Dorm.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.BLL.Services
{
    internal class JwtService
    {
        public string GetToken(User user)
        {

            var token = new JwtSecurityToken(
                expires: DateTime.Now,
                claims: );

            return token.;
        }
    }
}

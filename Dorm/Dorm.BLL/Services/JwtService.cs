using Dorm.BLL.Settings;
using Dorm.Domain.Entities.UserEF;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
// ReSharper disable UnusedVariable

namespace Dorm.BLL.Services
{
    /// <summary>
    /// Service for handling JWT (JSON Web Token) operations.
    /// </summary>
    public class JwtService(IOptions<AuthSettings> options)
    {
        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <returns>The generated JWT token as a string.</returns>
        /// <exception cref="Exception">Thrown when the secret key is null.</exception>
        public string GetToken(UserEF user)
        {
            var claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim("role", user.UserType.ToString())
            };

            var signingKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(options.Value.SecretKey! ?? throw new Exception("Secret key is null")));
            var token = new JwtSecurityToken(
                expires: DateTime.Now.Add(options.Value.TimeExp),
                claims: claims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Extracts the user ID from the specified JWT token.
        /// </summary>
        /// <param name="token">The JWT token.</param>
        /// <returns>The user ID as a string, or null if not found.</returns>
        /// <exception cref="Exception">Thrown when the secret key is null.</exception>
        public string? GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(
                options.Value.SecretKey! ?? throw new Exception(
                    "Secret key is null")
            );

            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            return principal.FindFirst("id")?.Value;
        }
        
        /// <summary>
        /// Extracts the user role from the specified JWT token.
        /// </summary>
        /// <param name="token">The JWT token.</param>
        /// <returns>The user role as a string, or null if not found.</returns>
        /// <exception cref="Exception">Thrown when the secret key is null.</exception>
        public string? GetUserRoleFromToken(string token)
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

            return principal.FindFirst("role")?.Value;
        }
    }
}
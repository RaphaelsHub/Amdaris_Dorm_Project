using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dorm.Server.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginDto">The login DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the login operation.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var validationResponse = await _authService.AuthValidation(loginDto);

            if (!validationResponse.IsValid)
                return BadRequest(validationResponse);

            var authResponse = await _authService.LoginUser(loginDto);

            if (authResponse.Success && !string.IsNullOrEmpty(authResponse.Token) && !string.IsNullOrEmpty(authResponse.Role))
            {
                SetJwtCookie(authResponse.Token);
                AddUserRole(authResponse.Role);
                return Ok(authResponse);
            }

            return BadRequest(authResponse);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registrationDto">The registration DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the registration operation.</returns>
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationDto registrationDto)
        {
            var validationResponse = await _authService.AuthValidation(registrationDto);

            if (!validationResponse.IsValid)
                return BadRequest(validationResponse);

            var authResponse = await _authService.RegisterUser(registrationDto);

            return authResponse.Success ? Ok(authResponse) : BadRequest(authResponse);
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the result of the logout operation.</returns>
        [HttpPost("logout")]
        public Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("authToken");
            return Task.FromResult<IActionResult>(Ok());
        }

        /// <summary>
        /// Sets the JWT token as a cookie in the HTTP response.
        /// </summary>
        /// <param name="token">The JWT token.</param>
        private void SetJwtCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("authToken", token, cookieOptions);
        }

        /// <summary>
        /// Adds a role to the current user's claims.
        /// </summary>
        /// <param name="role">The role to add.</param>
        private void AddUserRole(string role)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null && !identity.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == role))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
        }
    }
}
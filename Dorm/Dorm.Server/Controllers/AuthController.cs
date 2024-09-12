using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Models;
using Microsoft.AspNetCore.Mvc;


namespace Dorm.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var validationResponse = await _authService.AuthValidation(loginModel);

            if (validationResponse.IsValid)
            {
                var authResponse = await _authService.LoginUser(loginModel);

                SetJwtCookie(authResponse.Token);

                return authResponse.Success ? Ok(authResponse) : BadRequest(authResponse);
            }

            return BadRequest(validationResponse);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationModel registrationModel)
        {
            var validationResponse = await _authService.AuthValidation(registrationModel);

            if (validationResponse.IsValid)
            {
                var authResponse = await _authService.RegisterUser(registrationModel);

                return authResponse.Success ? Ok(authResponse) : BadRequest(authResponse);
            }

            return BadRequest(validationResponse);
        }

        private void SetJwtCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("authToken", token, cookieOptions);
        }
    }
}

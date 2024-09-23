using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO.Auth;
using Microsoft.AspNetCore.Mvc;


namespace Dorm.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var validationResponse = await authService.AuthValidation(loginDto);

            if (!validationResponse.IsValid)
                return BadRequest(validationResponse);


            var authResponse = await authService.LoginUser(loginDto);

            // ReSharper disable once MergeIntoPattern
            if (authResponse.Success && !string.IsNullOrEmpty(authResponse.Token))
            {
                SetJwtCookie(authResponse.Token);
                return Ok(authResponse);
            }
            
            return BadRequest(authResponse);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationDto registrationDto)
        {
            var validationResponse = await authService.AuthValidation(registrationDto);

            if (validationResponse.IsValid)
            {
                var authResponse = await authService.RegisterUser(registrationDto);

                return authResponse.Success ? Ok(authResponse) : BadRequest(authResponse);
            }

            return BadRequest(validationResponse);
        }

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
        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("authToken");
            return Ok();
        }
    }
}
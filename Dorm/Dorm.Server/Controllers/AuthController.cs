using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Models;
using Microsoft.AspNetCore.Mvc;


namespace Dorm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(Name = "login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var validationResponse = await _authService.AuthValidation(loginModel);

            if (validationResponse.IsValid)
            {
                var authResponse = await _authService.LoginUser(loginModel);

                return authResponse.Success ? Ok(authResponse) : BadRequest(authResponse);
            }

            return BadRequest(validationResponse);
        }

        [HttpPost(Name = "registration")]
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
    }
}

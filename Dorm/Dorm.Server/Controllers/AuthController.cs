using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


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
        private void ValidateRequest(IValidatableObject request)
        {
            var validationContext = new ValidationContext(request);
            var validationResults = request.Validate(validationContext);

            foreach (var validationResult in validationResults)
            {
                var memberName = validationResult.MemberNames.FirstOrDefault() ?? string.Empty;
                var errorMessage = validationResult.ErrorMessage ?? string.Empty;
                ModelState.AddModelError(memberName, errorMessage);
            }
        }

        [HttpPost(Name = "login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            ValidateRequest(loginModel);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDto userDto =await _authService.LoginUser(loginModel);

            return Ok(new { success = true, userDto });
        }

        [HttpPost(Name = "registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationModel registrationModel)
        {
            ValidateRequest(registrationModel);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDto userDto =await _authService.RegisterUser(registrationModel);

            return Ok(new { success = true, userDto });
        }


    }
}

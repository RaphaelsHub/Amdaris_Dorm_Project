using Dorm.Server.RequestData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using LoginRequest = Dorm.Server.RequestData.LoginRequest;
using IdentityLoginRequest = Microsoft.AspNetCore.Identity.Data.LoginRequest;


namespace Dorm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {

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
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            ValidateRequest(loginRequest);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Запросы определенные

            return Ok();
        }

        [HttpPost(Name = "registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest registrationRequest)
        {
            ValidateRequest(registrationRequest);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Какие-то там обращения к базе

            return Ok();
        }


    }
}

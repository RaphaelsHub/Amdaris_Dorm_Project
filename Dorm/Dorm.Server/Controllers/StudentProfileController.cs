using Dorm.BLL.Settings;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dorm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentProfileController : ControllerBase
    {
        private readonly IStudentProfileService _studentProfileService;

        private readonly IOptions<AuthSettings> _options;

        public StudentProfileController(IStudentProfileService studentProfileService, IOptions<AuthSettings> options)
        {
            _studentProfileService = studentProfileService;
            _options = options;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile(UserProfileDto userDto) =>
            await _studentProfileService.Create(userDto) is var result && result.Data != null
                ? Ok(result.Data)
                : BadRequest(result.Description);

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProfileById(int id) =>
        //    await _studentProfileService.GetById(id) is var result && result.Data != null
        //    ? Ok(result.Data)
        //    : BadRequest(result.Description);

        [HttpGet]
        public async Task<IActionResult> GetProfileById()
        {
            var token = Request.Cookies["authToken"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.Value.SecretKey);

            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            var userIdClaim = principal.FindFirst("id")?.Value;

            var id = int.Parse(userIdClaim);

            return await _studentProfileService.GetById(id) is var result && result.Data != null
            ? Ok(result.Data)
            : BadRequest(result.Description);
        }
        
        
        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileDto userDto)
        {
            var token = Request.Cookies["authToken"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.Value.SecretKey);

            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            var userIdClaim = principal.FindFirst("id")?.Value;

            var id = int.Parse(userIdClaim);

            return await _studentProfileService.Edit(id, userDto) is var result && result.Data != null
                ? Ok(result.Data)
                : BadRequest(result.Description);

        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile([FromRoute] int id) =>
            await _studentProfileService.Delete(id) is var result && result.Data == true
                ? Ok(result.Data)
                : BadRequest(result.Description);

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProfiles()
        {
            var response = await _studentProfileService.GetAll();

            if (response.Data == null || response.Data.Count() == 0)
                return Ok(response.Description);

            return Ok(response.Data);
        }
    }
}

using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dorm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentProfileController : ControllerBase
    {
        private readonly IStudentProfileService _studentProfileService;

        public StudentProfileController(IStudentProfileService studentProfileService)
        {
            _studentProfileService = studentProfileService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile(UserProfileDto userDto) =>
            await _studentProfileService.Create(userDto) is var result && result.Data != null
                ? Ok(result.Data)
                : BadRequest(result.Description);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById(int id) =>
            await _studentProfileService.GetById(id) is var result && result.Data != null
            ? Ok(result.Data)
            : BadRequest(result.Description);

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile([FromRoute] int id, [FromBody] UserProfileDto userDto) =>
            await _studentProfileService.Edit(id, userDto) is var result && result.Data != null
                ? Ok(result.Data)
                : BadRequest(result.Description);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile([FromRoute] int id) =>
            await _studentProfileService.Delete(id) is var result && result.Data == true
                ? Ok(result.Data)
                : BadRequest(result.Description);

        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var response = await _studentProfileService.GetAll();

            if (response.Data == null || response.Data.Count() == 0)
                return Ok(response.Description);

            return Ok(response.Data);
        }
    }
}

using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable MergeIntoPattern

namespace Dorm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentProfileController(IStudentProfileService studentProfileService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            var response = await studentProfileService.GetById(id);
            
            if(response == null)
                throw new ArgumentNullException($"Response is null api/StudentProfileController/GetProfileById/{id}");
            
            return response.Data == null ? BadRequest(response.Description) : Ok(response.Data);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            BaseResponse<IEnumerable<UserProfileDto>> response = await studentProfileService.GetAll();

            if (response == null)
                throw new ArgumentNullException($"Response is null api/StudentProfileController/GetAllProfiles");
            
            return response.Data == null || !response.Data.Any() ? Ok(response.Description) : Ok(response.Data);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile([FromRoute] int id, [FromBody] UserProfileDto userDto)
        {
            var response = await studentProfileService.Edit(id, userDto);
            
            if(response == null)
                throw new ArgumentNullException($"Response is null api/StudentProfileController/UpdateProfile/{id}");
            
            return response.Data == null ? BadRequest(response.Description) : Ok(response.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile([FromRoute] int id)
        {
            var response = await studentProfileService.Delete(id);
            
            if(response == null)
                throw new ArgumentNullException($"Response is null api/StudentProfileController/DeleteProfile/{id}");
            
            return response.Data ? Ok(response.Description) : BadRequest(response.Description);
        }
    }
}

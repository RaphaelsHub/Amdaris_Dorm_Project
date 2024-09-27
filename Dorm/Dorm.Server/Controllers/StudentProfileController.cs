using Dorm.BLL.Interfaces;
using Dorm.BLL.Services;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using Dorm.Server.Contracts.Commands.StudentProfile.Delete;
using Dorm.Server.Contracts.Commands.StudentProfile.Update;
using Dorm.Server.Contracts.Queries.StudentProfile.Get;
using Dorm.Server.Contracts.Queries.StudentProfile.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
// ReSharper disable MergeIntoPattern

namespace Dorm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentProfileController(IStudentProfileService studentProfileService, 
        IMediator mediator, IOptions<AuthSettings> options) : ControllerBase
    {
        public IStudentProfileService StudentProfileService { get; } = studentProfileService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            try
            {
                var response = await mediator.Send(new GetStudentProfileQuery(id, GetToken())); 
                
                if(response == null)
                    throw new ArgumentNullException($"Response is null api/StudentProfileController/GetProfileById/{id}");
                
                return response.Data == null ? BadRequest(response.Description) : Ok(response.Data);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var token = Request.Cookies["authToken"];

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized("Token is missing");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(options.Value.SecretKey);

                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);

                var userIdClaim = principal.FindFirst("id")?.Value;
                var id = int.Parse(userIdClaim);

                var response = await mediator.Send(new GetStudentProfileQuery(id, GetToken()));

                if (response == null)
                    throw new ArgumentNullException($"Response is null api/StudentProfileController/GetProfileById/{id}");

                return response.Data == null ? BadRequest(response.Description) : Ok(response.Data);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllProfiles()
        {
            try
            {
                var response = await mediator.Send(new GetAllStudentProfilesQuery(GetToken()));
                
                if (response == null)
                    throw new ArgumentNullException($"Response is null api/StudentProfileController/GetAllProfiles");
                
                return response.Data == null || !response.Data.Any() ? Ok(response.Description) : Ok(response.Data);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateProfile([FromRoute] int id, [FromBody] UserProfileDto userDto)
        //{
        //    try
        //    {
        //        var response = await mediator.Send(new UpdateStudentProfileCommand(id, userDto, GetToken()));

        //        if(response == null)
        //            throw new ArgumentNullException($"Response is null api/StudentProfileController/UpdateProfile/{id}");

        //        return response.Data == null ? BadRequest(response.Description) : Ok(response.Data);
        //    }
        //    catch (UnauthorizedAccessException ex)
        //    {
        //        return Unauthorized(ex.Message);
        //    }
        //    catch(ArgumentNullException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileDto userDto)
        {
            var token = Request.Cookies["authToken"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(options.Value.SecretKey);

            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            var userIdClaim = principal.FindFirst("id")?.Value;

            var id = int.Parse(userIdClaim);

            return await studentProfileService.Edit(id, userDto) is var result && result.Data != null
                ? Ok(result.Data)
                : BadRequest(result.Description);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile([FromRoute] int id)
        {
            try
            {
                var response = await mediator.Send(
                    new DeleteStudentProfileCommand(id, GetToken()));

                if (response == null)
                    throw new ArgumentNullException(
                        $"Response is null api/StudentProfileController/DeleteProfile/{id}");

                return response.Data ? Ok(response.Description) : BadRequest(response.Description);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        private string GetToken()
        {
            var token = Request.Cookies["authToken"] ?? string.Empty;

            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Token is missing");

            return token;
        }
    }
}

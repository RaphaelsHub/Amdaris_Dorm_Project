using Dorm.Domain.DTO;
using Dorm.Server.Contracts.Commands.StudentProfile.Delete;
using Dorm.Server.Contracts.Commands.StudentProfile.Update;
using Dorm.Server.Contracts.Queries.StudentProfile.Get;
using Dorm.Server.Contracts.Queries.StudentProfile.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable MergeIntoPattern
// ReSharper disable UnusedVariable

namespace Dorm.Server.Controllers
{
    /// <summary>
    /// Controller for handling student profile operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentProfileController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public StudentProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the profile of the current user.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the response is null.</exception>
        [HttpGet]
        [Authorize(Policy = "Student")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var response = await _mediator.Send(new GetStudentProfileQuery(GetToken())); 
                
                if(response == null)
                    throw new ArgumentNullException($"Response is null api/StudentProfileController/GetProfileById/");
                
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
        
        /// <summary>
        /// Gets all profiles.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the response is null.</exception>
        [HttpGet("getall")]
        [Authorize(Policy = "Student")]
        public async Task<IActionResult> GetAllProfiles()
        {
            try
            {
                var response = await _mediator.Send(new GetAllStudentProfilesQuery(GetToken()));
                
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
        
        /// <summary>
        /// Updates the profile of the current user.
        /// </summary>
        /// <param name="userDto">The user profile DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the response is null.</exception>
        [HttpPut]
        [Authorize(Policy = "Student")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileDto userDto)
        {
            try
            {
                var response = await _mediator.Send(new UpdateStudentProfileCommand(userDto, GetToken()));
                
                if(response == null)
                    throw new ArgumentNullException($"Response is null api/StudentProfileController/UpdateProfile/");
                
                return response.Data == null ? BadRequest(response.Description) : Ok(response.Data);
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

        /// <summary>
        /// Deletes the profile of the current user.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the response is null.</exception>
        [HttpDelete]
        public async Task<IActionResult> DeleteProfile()
        {
            try
            {
                var response = await _mediator.Send(
                    new DeleteStudentProfileCommand(GetToken()));

                if (response == null)
                    throw new ArgumentNullException(
                        $"Response is null api/StudentProfileController/DeleteProfile");

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
        
        /// <summary>
        /// Gets the JWT token from the request cookies.
        /// </summary>
        /// <returns>The JWT token as a string.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the token is missing.</exception>
        private string GetToken()
        {
            var token = Request.Cookies["authToken"] ?? string.Empty;

            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Token is missing");

            return token;
        }
    }
}
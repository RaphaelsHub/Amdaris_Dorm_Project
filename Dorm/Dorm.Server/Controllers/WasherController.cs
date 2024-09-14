using Dorm.Domain.DTO.Laundry;
using Dorm.Server.Contracts.Commands.Washer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dorm.Server.Controllers
{
    [ApiController]
    [Route("api/washers")]
    public class WasherController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WasherController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationDto reservationDto)
        {
            var token = Request.Cookies["authToken"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            var response = await _mediator.Send(new CreateReservationCommand(reservationDto, token));
            if(response.Data == null)
            {
                return BadRequest(response.Description);
            }
            return Ok(response.Data);
        }
    }
}

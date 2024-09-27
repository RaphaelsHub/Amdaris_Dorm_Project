using Dorm.Domain.DTO.Laundry;
using Dorm.Server.Contracts.Commands.Reservation.Create;
using Dorm.Server.Contracts.Commands.Reservation.Delete;
using Dorm.Server.Contracts.Queries.Reservation.GetAll;
using Dorm.Server.Contracts.Queries.Reservation.GetAllByUserId;
using Dorm.Server.Contracts.Queries.Reservation.GetAllByWasherId;
using Dorm.Server.Contracts.Queries.Reservation.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dorm.Server.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{reservationId}")]
        public async Task<IActionResult> GetById([FromRoute] int reservationId)
        {
            var response = await _mediator.Send(new GetByIdQuery(reservationId));

            if (response.Data == null)
            {
                return NotFound(response.Description);
            }

            return Ok(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var response = await _mediator.Send(new GetAllReservationsQuery());

            if (response.Data == null)
            {
                return NotFound(response.Description);
            }

            return Ok(response.Data);
        }

        [HttpGet("byUser/{userId}")]
        public async Task<IActionResult> GetAllByUserId([FromRoute] int userId)
        {
            var response = await _mediator.Send(new GetAllByUserIdQuery(userId));

            if (response.Data == null)
            {
                return NotFound(response.Description);
            }

            return Ok(response.Data);
        }

        [HttpGet("byWasher/{washerId}")]
        public async Task<IActionResult> GetAllByWasherId([FromRoute] int washerId)
        {
            var response = await _mediator.Send(new GetAllByWasherIdQuery(washerId));

            if (response.Data == null)
            {
                return NotFound(response.Description);
            }

            return Ok(response.Data);
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

        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> Delete([FromRoute] int reservationId)
        {
            var token = Request.Cookies["authToken"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            var response = await _mediator.Send(new DeleteReservationCommand(reservationId, token));
            if (response.Data)
                return NoContent();

            return NotFound(response.Description);
        }
    }
}

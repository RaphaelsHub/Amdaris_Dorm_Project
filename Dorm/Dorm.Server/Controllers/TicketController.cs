using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Server.Contracts.Commands.Ticket.Create;
using Dorm.Server.Contracts.Commands.Ticket.Delete;
using Dorm.Server.Contracts.Commands.Ticket.Update;
using Dorm.Server.Contracts.Queries.Ticket.Get;
using Dorm.Server.Contracts.Queries.Ticket.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dorm.Server.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    //[Authorize]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetTicketById([FromRoute] int ticketId)
        {
            var token = Request.Cookies["authToken"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            var response = await _mediator.Send(new GetTicketByIdQuery(ticketId, token));
            if (response.Data == null)
                return NotFound(response.Description);

            return Ok(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var response = await _mediator.Send(new GetAllTicketsQuery());

            if(response.Data == null)
            {
                return NotFound(response.Description);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketDto ticketDto)
        {
            var token = Request.Cookies["authToken"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            var response = await _mediator.Send(new CreateTicketCommand(ticketDto, token));
            if (response.Data == null)
            {
                return NotFound(response.Description);
            }
            return CreatedAtAction(nameof(GetTicketById), new { ticketId = response.Data.Id }, response.Data);
        }

        [HttpDelete("{ticketId}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] int ticketId)
        {
            var response = await _mediator.Send(new DeleteTicketCommand(ticketId));
            if(response.Data)
                return NoContent();

            return NotFound(response.Description);
        }

        [HttpPut("{ticketId}")]
        public async Task<IActionResult> UpdateTicket([FromRoute] int ticketId, [FromBody] TicketDto ticketDto)
        {
            var token = Request.Cookies["authToken"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            var response = await _mediator.Send(new UpdateTicketCommand(ticketId, ticketDto, token));
            if (response.Data == null)
                return NotFound(response.Description);

            return Ok(response.Data);
        }
    }
}

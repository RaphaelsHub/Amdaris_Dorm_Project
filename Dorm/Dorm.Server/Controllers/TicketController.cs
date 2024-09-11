using Dorm.BLL.Interfaces;
using Dorm.Domain.Entities.Ticket;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dorm.Server.Controllers
{
    [ApiController]
    [Route("tickets")]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITicketService _ticketService;

        public TicketController(IMediator mediator, ITicketService ticketService)
        {
            _mediator = mediator;
            _ticketService = ticketService;
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetTicketById([FromRoute] int ticketId)
        {
            //TicketDto ticket = await _mediator.Send(new GetTicketByIdQuery(ticketId));
            Ticket ticket = await _ticketService.GetTicketById(ticketId);
            return Ok(ticket);
        }
    }
}

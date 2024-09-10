using Dorm.Domain.Contracts.Queries;
using Dorm.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dorm.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : Controller
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetTicketById([FromRoute] int ticketId)
        {
            TicketDto ticket = await _mediator.Send(new GetTicketByIdQuery(ticketId));
            return Ok(ticket);
        }
    }
}

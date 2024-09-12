﻿using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ticket;
using Dorm.Server.Contracts.Commands;
using Dorm.Server.Contracts.Commands.TicketCommands;
using Dorm.Server.Contracts.Queries.TicketQueries;
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
        private readonly ITicketService _ticketService;

        public TicketController(IMediator mediator, ITicketService ticketService)
        {
            _mediator = mediator;
            _ticketService = ticketService;
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetTicketById([FromRoute] int ticketId)
        {
            TicketDto? ticket = await _mediator.Send(new GetTicketByIdQuery(ticketId));
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketCommand createTicketCommand)
        {
            TicketDto ticket = await _mediator.Send(createTicketCommand);
            return CreatedAtAction(nameof(GetTicketById), new { ticketId = ticket.Id }, ticket);
        }

        [HttpDelete("{ticketId}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] int ticketId)
        {
            return await _mediator.Send(new DeleteTicketCommand(ticketId)) ? NoContent() : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTicket([FromBody] UpdateTicketCommand updateTicketCommand)
        {
            TicketDto updatedTicket = await _mediator.Send(updateTicketCommand);
            if (updatedTicket == null)
                return NotFound();

            return Ok(updatedTicket);
        }
    }
}

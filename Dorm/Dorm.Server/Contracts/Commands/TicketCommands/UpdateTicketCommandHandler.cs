using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ticket;
using MediatR;

namespace Dorm.Server.Contracts.Commands.TicketCommands
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, TicketDto>
    {
        private readonly ITicketService _ticketService;
        public UpdateTicketCommandHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<TicketDto> Handle(UpdateTicketCommand updateTicketCommand, CancellationToken cancellationToken)
        {
            var updatedTicket = await _ticketService.UpdateTicket(updateTicketCommand.ticketDto);
            return new TicketDto
            {
                Id = updatedTicket.Id,
                Name = updatedTicket.Name,
                Group = updatedTicket.Group,
                Room = updatedTicket.Room,
                Type = updatedTicket.Type,
                Subject = updatedTicket.Subject,
                Description = updatedTicket.Description,
                Status = updatedTicket.Status,
                //Respondent = _userService.GetUserById(updatedTicket.RespondentId),
                Date = updatedTicket.Date,
                Response = updatedTicket.Response
            };
        }
    }
}

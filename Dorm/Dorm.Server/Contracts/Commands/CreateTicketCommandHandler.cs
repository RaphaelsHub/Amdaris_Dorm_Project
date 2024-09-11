using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ticket;
using MediatR;

namespace Dorm.Server.Contracts.Commands
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, TicketDto>
    {
        private readonly ITicketService _ticketService;
        public CreateTicketCommandHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<TicketDto> Handle(CreateTicketCommand createTicketCommand, CancellationToken cancellationToken)
        {
            Ticket ticket = new Ticket
            {
                Name = createTicketCommand.ticketDto.Name,
                Group = createTicketCommand.ticketDto.Group,
                Room = createTicketCommand.ticketDto.Room,
                Type = createTicketCommand.ticketDto.Type,
                Subject = createTicketCommand.ticketDto.Subject,
                Description = createTicketCommand.ticketDto.Description,
                RespondentId = createTicketCommand.ticketDto.Respondent.Id,
                Date = createTicketCommand.ticketDto.Date,
                Response = createTicketCommand.ticketDto.Response,
                Status = Domain.Enum.TicketStatus.SENT
            };

            var createdTicket = await _ticketService.CreateTicket(ticket) ?? throw new Exception("Failed to create ticket");
            
            TicketDto ticketDto = createTicketCommand.ticketDto;
            ticketDto.Id = createdTicket.Id;
            return ticketDto;
        }
    }
}

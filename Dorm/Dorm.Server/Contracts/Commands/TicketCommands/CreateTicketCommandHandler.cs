using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ticket;
using MediatR;

namespace Dorm.Server.Contracts.Commands.TicketCommands
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
            var createdTicket = await _ticketService.CreateTicket(createTicketCommand.ticketDto) ?? throw new Exception("Failed to create ticket");
            TicketDto ticketDto = createTicketCommand.ticketDto;
            ticketDto.Id = createdTicket.Id;
            return ticketDto;
        }
    }
}

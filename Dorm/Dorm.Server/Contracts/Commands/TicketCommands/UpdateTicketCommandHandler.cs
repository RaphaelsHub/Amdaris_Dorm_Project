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
            return await _ticketService.Update(updateTicketCommand.ticketId, updateTicketCommand.ticketDto);
        }
    }
}

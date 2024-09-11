using Dorm.BLL.Interfaces;
using MediatR;

namespace Dorm.Server.Contracts.Commands
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, bool>
    {
        private readonly ITicketService _ticketService;
        public DeleteTicketCommandHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<bool> Handle(DeleteTicketCommand deleteTicketCommand, CancellationToken cancellationToken)
        {
            return await _ticketService.DeleteTicket(deleteTicketCommand.ticketId);
        }
    }
}

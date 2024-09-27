using Dorm.BLL.Interfaces;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.Ticket.Delete
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, TestsResponse<bool>>
    {
        private readonly ITicketService _ticketService;
        public DeleteTicketCommandHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<TestsResponse<bool>> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            return await _ticketService.Delete(request.ticketId);
        }
    }
}

using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.Ticket.Update
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, BaseResponse<TicketDto>>
    {
        private readonly ITicketService _ticketService;
        public UpdateTicketCommandHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<BaseResponse<TicketDto>> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            return await _ticketService.Update(request.ticketId, request.ticketDto);
        }
    }
}

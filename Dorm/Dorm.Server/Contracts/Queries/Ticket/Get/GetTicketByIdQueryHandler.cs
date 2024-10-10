using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Ticket.Get
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, BaseResponse<TicketDto>>
    {
        private readonly ITicketService _ticketService;

        public GetTicketByIdQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<BaseResponse<TicketDto>> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            return await _ticketService.GetById(request.ticketId);
        }
    }
}

using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Ticket.GetAll
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, BaseResponse<IEnumerable<TicketDto>>>
    {
        private readonly ITicketService _ticketService;
        public GetAllTicketsQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<BaseResponse<IEnumerable<TicketDto>>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            return await _ticketService.GetAll();
        }
    }
}

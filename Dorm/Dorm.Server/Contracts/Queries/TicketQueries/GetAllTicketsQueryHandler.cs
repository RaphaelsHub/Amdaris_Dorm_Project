using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using MediatR;

namespace Dorm.Server.Contracts.Queries.TicketQueries
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketDto>>
    {
        private readonly ITicketService _ticketService;
        public GetAllTicketsQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<IEnumerable<TicketDto>> Handle(GetAllTicketsQuery getAllTicketsQuery, CancellationToken cancellationToken)
        {
            return await _ticketService.GetAll();
        }
    }
}

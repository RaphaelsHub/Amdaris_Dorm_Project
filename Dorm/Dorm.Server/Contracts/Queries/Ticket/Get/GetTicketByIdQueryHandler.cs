using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.Server.Contracts.Queries.Ticket.Get
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto?>
    {
        private readonly ITicketService _ticketService;

        public GetTicketByIdQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<TicketDto?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            return await _ticketService.GetById(request.ticketId);
        }
    }
}

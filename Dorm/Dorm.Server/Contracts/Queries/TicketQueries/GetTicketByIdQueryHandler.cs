using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ticket;
using Dorm.Server.Contracts.Queries.TicketQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.Domain.Contracts.Queries
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto?>
    {
        //private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;

        public GetTicketByIdQueryHandler(/*IMapper mapper,*/ ITicketService ticketService)
        {
            //_mapper = mapper;
            _ticketService = ticketService;
        }
        public async Task<TicketDto?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            return await _ticketService.GetTicketById(request.ticketId);
        }
    }
}

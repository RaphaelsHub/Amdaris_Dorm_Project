using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ticket;
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
            Ticket? ticket = await _ticketService.GetTicketById(request.ticketId);

            //return _mapper.Map<TicketDto>(ticket);
            if (ticket != null)
            {
                return new TicketDto
                {
                    Id = ticket.Id,
                    Name = ticket.Name,
                    Group = ticket.Group,
                    Room = ticket.Room,
                    Type = ticket.Type,
                    Subject = ticket.Subject,
                    Description = ticket.Description,
                    //Respondent = _userService.GetUserById(ticket.RespondentId),
                    Date = ticket.Date,
                    Response = ticket.Response
                };
            }
            return null;
        }
    }
}

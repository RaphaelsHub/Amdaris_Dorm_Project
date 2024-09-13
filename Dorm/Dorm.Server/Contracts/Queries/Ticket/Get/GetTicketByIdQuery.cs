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
    public record GetTicketByIdQuery(int ticketId) : IRequest<TicketDto?>;
}

using Dorm.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.Domain.Contracts.Queries
{
    public record GetTicketByIdQuery(int ticketId) : IRequest<TicketDto>;
}

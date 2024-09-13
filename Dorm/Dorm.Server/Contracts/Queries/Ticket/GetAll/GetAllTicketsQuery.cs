using MediatR;
using Dorm.Domain.DTO;

namespace Dorm.Server.Contracts.Queries.Ticket.GetAll
{
    public record GetAllTicketsQuery() : IRequest<IEnumerable<TicketDto>>;
}

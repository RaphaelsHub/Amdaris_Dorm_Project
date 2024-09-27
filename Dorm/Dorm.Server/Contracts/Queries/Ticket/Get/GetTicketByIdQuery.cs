using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Ticket.Get
{
    public record GetTicketByIdQuery(int ticketId, string token) : IRequest<TestsResponse<TicketDto>>;
}

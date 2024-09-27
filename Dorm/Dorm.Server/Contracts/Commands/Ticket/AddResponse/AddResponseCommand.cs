using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.Ticket.AddResponse
{
    public record AddResponseCommand (int ticketId, TicketDto ticketDto, string token) : IRequest<TestsResponse<TicketDto>>;
}

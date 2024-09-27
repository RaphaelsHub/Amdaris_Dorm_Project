using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.Ticket.Update
{
    public record UpdateTicketCommand(int ticketId, TicketDto ticketDto, string token) : IRequest<TestsResponse<TicketDto>>;
}

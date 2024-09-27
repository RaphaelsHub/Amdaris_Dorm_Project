using MediatR;
using Dorm.Domain.Responces;

namespace Dorm.Server.Contracts.Commands.Ticket.Delete
{
    public record DeleteTicketCommand(int ticketId) : IRequest<TestsResponse<bool>>;
}

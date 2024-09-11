using MediatR;
using Dorm.Domain.Entities.Ticket;

namespace Dorm.Server.Contracts.Commands
{
    public record DeleteTicketCommand(int ticketId) : IRequest<bool>;
}

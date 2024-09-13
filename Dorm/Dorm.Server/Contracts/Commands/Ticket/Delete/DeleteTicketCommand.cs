using MediatR;
using Dorm.Domain.Entities.Ticket;

namespace Dorm.Server.Contracts.Commands.Ticket.Delete
{
    public record DeleteTicketCommand(int ticketId) : IRequest<bool>;
}

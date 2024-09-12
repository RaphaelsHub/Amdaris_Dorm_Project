using MediatR;
using Dorm.Domain.Entities.Ticket;

namespace Dorm.Server.Contracts.Commands.TicketCommands
{
    public record DeleteTicketCommand(int ticketId) : IRequest<bool>;
}

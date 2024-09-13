using Dorm.Domain.DTO;
using MediatR;

namespace Dorm.Server.Contracts.Commands.Ticket.Update
{
    public record UpdateTicketCommand(int ticketId, TicketDto ticketDto) : IRequest<TicketDto>;
}

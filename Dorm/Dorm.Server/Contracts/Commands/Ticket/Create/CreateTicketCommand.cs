using Dorm.Domain.DTO;
using MediatR;

namespace Dorm.Server.Contracts.Commands.Ticket.Create
{
    public record CreateTicketCommand(TicketDto ticketDto) : IRequest<TicketDto>;
}

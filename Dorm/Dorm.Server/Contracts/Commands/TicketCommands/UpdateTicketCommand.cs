using Dorm.Domain.DTO;
using MediatR;

namespace Dorm.Server.Contracts.Commands.TicketCommands
{
    public record UpdateTicketCommand(int ticketId, TicketDto ticketDto) : IRequest<TicketDto>;
}

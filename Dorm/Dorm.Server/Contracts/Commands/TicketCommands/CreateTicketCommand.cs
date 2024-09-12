using Dorm.Domain.DTO;
using MediatR;

namespace Dorm.Server.Contracts.Commands.TicketCommands
{
    public record CreateTicketCommand(TicketDto ticketDto) : IRequest<TicketDto>;
}

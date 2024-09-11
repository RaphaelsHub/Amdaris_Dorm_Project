using Dorm.Domain.DTO;
using MediatR;

namespace Dorm.Server.Contracts.Commands
{
    public record CreateTicketCommand(TicketDto ticketDto) : IRequest<TicketDto>;
}

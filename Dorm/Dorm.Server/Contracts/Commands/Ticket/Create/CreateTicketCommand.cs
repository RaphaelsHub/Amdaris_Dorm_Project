using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.Ticket.Create
{
    public record CreateTicketCommand(TicketDto ticketDto, string token) : IRequest<BaseResponse<TicketDto>>;
}

using MediatR;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;

namespace Dorm.Server.Contracts.Queries.Ticket.GetAll
{
    public record GetAllTicketsQuery() : IRequest<BaseResponse<IEnumerable<TicketDto>>>;
}

using MediatR;
using Dorm.Domain.DTO;

namespace Dorm.Server.Contracts.Queries.TicketQueries
{
    public record GetAllTicketsQuery() : IRequest<IEnumerable<TicketDto>>;
}

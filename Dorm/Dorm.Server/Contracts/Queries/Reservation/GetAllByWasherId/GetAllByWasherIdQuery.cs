using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Reservation.GetAllByWasherId
{
    public record GetAllByWasherIdQuery(int washerId) : IRequest<BaseResponse<IEnumerable<ReservationDto>>>;
}
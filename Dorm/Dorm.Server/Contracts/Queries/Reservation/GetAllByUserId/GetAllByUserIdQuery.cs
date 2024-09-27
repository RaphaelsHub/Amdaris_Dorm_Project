using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Reservation.GetAllByUserId
{
    public record GetAllByUserIdQuery(int userId) : IRequest<BaseResponse<IEnumerable<ReservationDto>>>;
}

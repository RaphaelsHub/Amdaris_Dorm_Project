using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Reservation.GetAll
{
    public record GetAllReservationsQuery() : IRequest<BaseResponse<IEnumerable<ReservationDto>>>;
}

using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Reservation.GetById
{
    public record GetByIdQuery(int reservationId) : IRequest<BaseResponse<ReservationDto>>;
}

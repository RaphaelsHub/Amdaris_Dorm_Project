using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.Reservation.Create
{
    public record CreateReservationCommand(ReservationDto reservationDto, string token) : IRequest<BaseResponse<ReservationDto>>;
}

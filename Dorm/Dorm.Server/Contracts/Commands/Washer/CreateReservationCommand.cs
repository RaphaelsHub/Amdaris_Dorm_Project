using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Entities.Laundry;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.Washer
{
    public record CreateReservationCommand(ReservationDto reservationDto, string token) : IRequest<BaseResponse<ReservationDto>>;
}

using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.Reservation.Delete
{
    public record DeleteReservationCommand(int reservationId, string token) : IRequest<BaseResponse<bool>>;
}

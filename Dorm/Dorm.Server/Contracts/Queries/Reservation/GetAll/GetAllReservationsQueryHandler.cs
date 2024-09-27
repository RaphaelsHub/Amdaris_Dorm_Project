using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Reservation.GetAll
{
    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, BaseResponse<IEnumerable<ReservationDto>>>
    {
        private readonly IWasherService _washerService;
        public GetAllReservationsQueryHandler(IWasherService washerService)
        {
            _washerService = washerService;
        }

        public async Task<BaseResponse<IEnumerable<ReservationDto>>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            return await _washerService.GetAll();
        }
    }
}

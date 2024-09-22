using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Reservation.GetAllByWasherId
{
    public class GetAllByWasherIdQueryHandler : IRequestHandler<GetAllByWasherIdQuery, BaseResponse<IEnumerable<ReservationDto>>>
    {
        private readonly IWasherService _washerService;
        public GetAllByWasherIdQueryHandler(IWasherService washerService)
        {
            _washerService = washerService;
        }

        public async Task<BaseResponse<IEnumerable<ReservationDto>>> Handle(GetAllByWasherIdQuery request, CancellationToken cancellationToken)
        {
            return await _washerService.GetAllByWasherId(request.washerId);
        }
    }
}

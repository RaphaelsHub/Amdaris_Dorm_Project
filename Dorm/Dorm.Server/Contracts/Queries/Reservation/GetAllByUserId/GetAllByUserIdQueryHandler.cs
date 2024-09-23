using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Reservation.GetAllByUserId
{
    public class GetAllByUserIdQueryHandler : IRequestHandler<GetAllByUserIdQuery, BaseResponse<IEnumerable<ReservationDto>>>
    {
        private readonly IWasherService _washerService;
        public GetAllByUserIdQueryHandler(IWasherService washerService)
        {
            _washerService = washerService;
        }

        public async Task<BaseResponse<IEnumerable<ReservationDto>>> Handle(GetAllByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _washerService.GetAllByUserId(request.userId);
        }
    }
}

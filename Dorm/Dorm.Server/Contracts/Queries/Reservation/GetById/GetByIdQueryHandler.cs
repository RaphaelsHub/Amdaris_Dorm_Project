using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Reservation.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, BaseResponse<ReservationDto>>
    {
        private readonly IWasherService _washerService;
        public GetByIdQueryHandler(IWasherService washerService)
        {
            _washerService = washerService;
        }

        public async Task<BaseResponse<ReservationDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _washerService.GetById(request.reservationId);
        }
    }
}

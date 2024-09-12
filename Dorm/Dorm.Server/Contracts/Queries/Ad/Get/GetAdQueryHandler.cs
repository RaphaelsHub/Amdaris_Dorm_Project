using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Ad.Get
{
    public class GetAdQueryHandler
        : IRequestHandler<GetAdQuery, BaseResponse<AdDto>>
    {
        private readonly IAdService _adService;

        public GetAdQueryHandler(IAdService adService)
        {
            _adService = adService;
        }

        public async Task<BaseResponse<AdDto>> Handle(GetAdQuery request, CancellationToken cancellationToken)
        {
            return await _adService.Get(request.adId);
        }
    }
}

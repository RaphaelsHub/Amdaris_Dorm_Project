using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Ad.GetAll
{
    public class GetAllAdQueryHandler
        : IRequestHandler<GetAllAdsQuery, BaseResponse<IEnumerable<AdDto>>>
    {
        private readonly IAdService _adService;

        public GetAllAdQueryHandler(IAdService adService)
        {
            _adService = adService;
        }

        public async Task<BaseResponse<IEnumerable<AdDto>>> Handle(GetAllAdsQuery request, CancellationToken cancellationToken)
        {
            return await _adService.GetAll();
        }
    }
}

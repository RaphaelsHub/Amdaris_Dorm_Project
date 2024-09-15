using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.Ad.Edit
{
    public class EditAdCommandHandler
        : IRequestHandler<EditAdCommand, BaseResponse<AdDto>>
    {
        private readonly IAdService _adService;

        public EditAdCommandHandler(IAdService adService)
        {
            _adService = adService;
        }

        public async Task<BaseResponse<AdDto>> Handle(EditAdCommand request, CancellationToken cancellationToken)
        {
            return await _adService.Edit(request.adId, request.model);
        }
    }
}

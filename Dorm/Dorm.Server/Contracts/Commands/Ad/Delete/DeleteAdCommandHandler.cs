using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;
using Microsoft.Extensions.Options;

namespace Dorm.Server.Contracts.Commands.Ad.Delete
{
    public class DeleteAdCommandHandler
        : IRequestHandler<DeleteAdCommand, TestsResponse<bool>>
    {
        private readonly IAdService _adService;

        public DeleteAdCommandHandler(IAdService adService)
        {
            _adService = adService;
        }

        public async Task<TestsResponse<bool>> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            return await _adService.Delete(request.adId);
        }
    }
}

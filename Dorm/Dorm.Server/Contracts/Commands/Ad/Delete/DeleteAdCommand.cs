using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.Ad.Delete
{
    public record DeleteAdCommand(int adId)
        : IRequest<BaseResponse<bool>>;
}

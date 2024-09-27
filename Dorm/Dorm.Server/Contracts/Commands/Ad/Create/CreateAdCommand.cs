using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.Ad.Create
{
    public record CreateAdCommand(AdDto model, string token) : IRequest<TestsResponse<AdDto>>;
}

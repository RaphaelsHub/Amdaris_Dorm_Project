using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Ad.GetAll
{
    public record GetAllAdsQuery()
        : IRequest<TestsResponse<IEnumerable<AdDto>>>;
}

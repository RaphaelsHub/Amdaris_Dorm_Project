using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dorm.Server.Contracts.Commands.Ad.Edit
{
    public record EditAdCommand(int adId, AdDto model, string token)
        : IRequest<BaseResponse<AdDto>>;
}

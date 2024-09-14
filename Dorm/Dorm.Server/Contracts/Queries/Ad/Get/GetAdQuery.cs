﻿using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.Ad.Get
{
    public record GetAdQuery(int adId)
        : IRequest<BaseResponse<AdDto>>;
}
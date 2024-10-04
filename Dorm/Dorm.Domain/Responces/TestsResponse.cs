using Dorm.Domain.Responces;
using System;

namespace Dorm.Domain.Responses
{
    public record TestsResponse<T>(T? Data, string? Description) : BaseResponse<T>(Data, Description)
    {
        public bool Success => Data != null;
    };
}

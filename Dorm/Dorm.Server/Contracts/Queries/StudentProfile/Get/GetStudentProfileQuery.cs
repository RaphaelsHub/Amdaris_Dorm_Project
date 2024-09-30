using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.StudentProfile.Get;

public record GetStudentProfileQuery(string Token) : 
            IRequest<BaseResponse<UserProfileDto>>;
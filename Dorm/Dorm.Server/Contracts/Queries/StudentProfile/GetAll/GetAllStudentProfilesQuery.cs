using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Queries.StudentProfile.GetAll;

public record GetAllStudentProfilesQuery(string Token) :
    IRequest<BaseResponse<IEnumerable<UserProfileDto>>>;
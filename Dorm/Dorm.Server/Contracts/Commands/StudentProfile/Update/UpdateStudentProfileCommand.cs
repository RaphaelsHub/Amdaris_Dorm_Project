using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.StudentProfile.Update;

public record UpdateStudentProfileCommand(int Id, UserProfileDto UserProfileDto, string Token) : 
            IRequest<BaseResponse<UserProfileDto>>;
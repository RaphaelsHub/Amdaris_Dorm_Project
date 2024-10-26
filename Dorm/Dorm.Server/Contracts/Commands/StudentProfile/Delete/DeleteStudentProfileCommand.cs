using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.StudentProfile.Delete;

public record DeleteStudentProfileCommand(string Token) 
        : IRequest<BaseResponse<bool>>;
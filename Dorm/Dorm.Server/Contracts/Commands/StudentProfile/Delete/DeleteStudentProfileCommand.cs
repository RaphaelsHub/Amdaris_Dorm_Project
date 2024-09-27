using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.StudentProfile.Delete;

public record DeleteStudentProfileCommand(int Id, string Token) 
        : IRequest<BaseResponse<bool>>;
using Dorm.BLL.Interfaces;
using Dorm.BLL.Services;
using Dorm.Domain.Responces;
using MediatR;

namespace Dorm.Server.Contracts.Commands.StudentProfile.Delete;

public class DeleteStudentProfileCommandHandler(IStudentProfileService studentProfileService, JwtService jwtService) : 
    IRequestHandler<DeleteStudentProfileCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(DeleteStudentProfileCommand request, 
        CancellationToken cancellationToken)
    {
        var userId = jwtService.GetUserIdFromToken(request.Token);
        
        if(string.IsNullOrEmpty(userId))
            return new BaseResponse<bool>(false,  "User not found because of invalid token");
        
        var userExists = await studentProfileService.GetById(int.Parse(userId)) != null;

        if (!userExists)
            return new BaseResponse<bool>(false,  "User not found because user dont exists");
        
        return await studentProfileService.Delete(int.Parse(userId));
    }
}
using Dorm.BLL.Interfaces;
using Dorm.BLL.Services;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;


namespace Dorm.Server.Contracts.Commands.StudentProfile.Update;

public class UpdateStudentProfileCommandHandler(IStudentProfileService studentProfileService, JwtService jwtService) :
    IRequestHandler<UpdateStudentProfileCommand, BaseResponse<UserProfileDto>>
{
    public async Task<BaseResponse<UserProfileDto>> Handle(UpdateStudentProfileCommand request, 
        CancellationToken cancellationToken)
    {
        var userId = jwtService.GetUserIdFromToken(request.Token);
        
        if(string.IsNullOrEmpty(userId))
            return new BaseResponse<UserProfileDto>(null,  "User not found because of invalid token");
        
        var userExists = await studentProfileService.GetById(int.Parse(userId)) != null;

        if (!userExists)
            return new BaseResponse<UserProfileDto>(null,  "User not found because user dont exists");
        
        return await studentProfileService.Edit(int.Parse(userId), request.UserProfileDto);
    }
}
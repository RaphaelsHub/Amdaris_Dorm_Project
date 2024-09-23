using Dorm.BLL.Interfaces;
using Dorm.BLL.Services;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;


namespace Dorm.Server.Contracts.Queries.StudentProfile.Get;

public class GetStudentProfileQueryHandler(IStudentProfileService studentProfileService, JwtService jwtService) :
    IRequestHandler<GetStudentProfileQuery, BaseResponse<UserProfileDto>>
{
    public async Task<BaseResponse<UserProfileDto>> Handle(GetStudentProfileQuery request, 
        CancellationToken cancellationToken)
    {
        var userId = jwtService.GetUserIdFromToken(request.Token);

        var userExists = userId != null && await studentProfileService.GetById(int.Parse(userId)) != null;

        if (!userExists)
            return new BaseResponse<UserProfileDto>(null,  "User not found because of invalid token");
        
        return await studentProfileService.GetById(request.Id);
    }
}
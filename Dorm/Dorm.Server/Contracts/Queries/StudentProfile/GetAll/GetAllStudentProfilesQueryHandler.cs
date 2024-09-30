using Dorm.BLL.Interfaces;
using Dorm.BLL.Services;
using MediatR;
using listUsers =
    Dorm.Domain.Responces.BaseResponse<System.Collections.Generic.IEnumerable<Dorm.Domain.DTO.UserProfileDto>>;

namespace Dorm.Server.Contracts.Queries.StudentProfile.GetAll;

public class GetAllStudentProfilesQueryHandle(IStudentProfileService studentProfileService, JwtService jwtService) :
    IRequestHandler<GetAllStudentProfilesQuery, listUsers>
{
    public async Task<listUsers> Handle(GetAllStudentProfilesQuery request,
        CancellationToken cancellationToken)
    {
        var userId = jwtService.GetUserIdFromToken(request.Token);
        
        if(string.IsNullOrEmpty(userId))
            return new listUsers(null, "User not found because of invalid token");

        var userExists = await studentProfileService.GetById(int.Parse(userId)) != null;

        if (!userExists)
            return new listUsers(null,  "User not found because of invalid token");
        
        return await studentProfileService.GetAll();
    }
}
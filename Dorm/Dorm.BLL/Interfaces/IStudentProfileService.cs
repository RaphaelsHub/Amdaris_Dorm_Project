using Dorm.Domain.DTO;
using Dorm.Domain.Responces;

namespace Dorm.Server.Controllers
{
    public interface IStudentProfileService
    {
        Task<TestsResponse<UserProfileDto>> Create(UserProfileDto model);
        Task<TestsResponse<UserProfileDto>> GetById(int id);
        Task<TestsResponse<UserProfileDto>> Edit(int id, UserProfileDto userDto);
        Task<TestsResponse<bool>> Delete(int id);
        Task<TestsResponse<IEnumerable<UserProfileDto>>> GetAll();
    }
}
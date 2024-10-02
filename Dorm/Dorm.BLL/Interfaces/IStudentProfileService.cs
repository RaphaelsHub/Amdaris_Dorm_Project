using Dorm.Domain.DTO;
using Dorm.Domain.Responces;

namespace Dorm.Server.Controllers
{
    public interface IStudentProfileService
    {
        Task<BaseResponse<UserProfileDto>> Create(UserProfileDto model);
        Task<BaseResponse<UserProfileDto>> GetById(int id);
        Task<BaseResponse<UserProfileDto>> Edit(int id, UserProfileDto userDto);
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<IEnumerable<UserProfileDto>>> GetAll();
    }
}
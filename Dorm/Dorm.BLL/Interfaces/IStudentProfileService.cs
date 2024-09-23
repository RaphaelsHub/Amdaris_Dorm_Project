using Dorm.Domain.DTO;
using Dorm.Domain.Responces;

namespace Dorm.BLL.Interfaces
{
    public interface IStudentProfileService
    {
        Task<BaseResponse<UserProfileDto>> GetById(int id);
        Task<BaseResponse<IEnumerable<UserProfileDto>>> GetAll();
        Task<BaseResponse<UserProfileDto>> Edit(int id, UserProfileDto userDto);
        Task<BaseResponse<bool>> Delete(int id);
    }
}
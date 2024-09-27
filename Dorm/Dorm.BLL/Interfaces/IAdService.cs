using Dorm.Domain.DTO;
using Dorm.Domain.Responces;

namespace Dorm.BLL.Interfaces
{
    public interface IAdService
    {
        Task<TestsResponse<AdDto>> Create(AdDto model);
        Task<TestsResponse<AdDto>> Edit(int id, AdDto model);
        Task<TestsResponse<bool>> Delete(int id);
        Task<TestsResponse<AdDto>> Get(int id);
        Task<TestsResponse<IEnumerable<AdDto>>> GetAll();
    }
}

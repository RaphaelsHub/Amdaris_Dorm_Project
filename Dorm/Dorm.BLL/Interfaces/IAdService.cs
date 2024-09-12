using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ad;
using Dorm.Domain.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.BLL.Interfaces
{
    public interface IAdService
    {
        Task<BaseResponse<AdDto>> Create(AdDto model);
        Task<BaseResponse<AdDto>> Edit(int id, AdDto model); 
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<AdDto>> Get(int id);
        Task<BaseResponse<IEnumerable<AdDto>>> GetAll();
    }
}

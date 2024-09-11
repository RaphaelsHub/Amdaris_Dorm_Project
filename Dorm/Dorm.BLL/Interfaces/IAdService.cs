using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.BLL.Interfaces
{
    public interface IAdService
    {
        Task<Ad> CreateAd(Ad model);
        Task<Ad> Edit(int id, Ad model); 
        Task<bool> DeleteAd(int id);
        Task<Ad> GetAdById(int id);
        Task<IEnumerable<Ad>> GetAllAds();
    }
}

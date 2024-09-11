using Dorm.BLL.Interfaces;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ad;

namespace Dorm.BLL.Services
{
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;
        public AdService(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public async Task<Ad> CreateAd(Ad model)
        {
            await _adRepository.Create(model);

            return model;
        }

        public async Task<bool> DeleteAd(int id)
        {
            var ad = await _adRepository.GetById(id);
            
            if (ad == null) 
            {
                ///
            }
            await _adRepository.Delete(ad);

            return true;
        }

        public async Task<Ad> Edit(int id, Ad model)
        {
            var ad = await _adRepository.GetById(id);

            if (ad == null)
            {
                ///
            }

            ad.Name = model.Name;

            await _adRepository.Update(ad);

            return ad;
        }

        public async Task<Ad> GetAdById(int id)
        {
            var ad = await _adRepository.GetById(id);

            if (ad == null)
            {
                ///
            }
            return ad;
        }

        public async Task<IEnumerable<Ad>> GetAllAds()
        {
            var ads = await _adRepository.GetAll();

            if (ads.Count() == 0)
            {
                ///
            }
            return ads;
        }
    }
}

using AutoMapper;
using Dorm.BLL.Interfaces;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ad;
using Dorm.Domain.Entities.Ticket;
using Dorm.Domain.Responces;

namespace Dorm.BLL.Services
{
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;
        private readonly IMapper _mapper;
        public AdService(IAdRepository adRepository, IMapper mapper)
        {
            _mapper = mapper;
            _adRepository = adRepository;
        }

        public async Task<TestsResponse<AdDto>> Create(AdDto model)
        {
            try
            {
                var ad = _mapper.Map<Ad>(model);
                ad.CreatedDate = DateTime.UtcNow;

                await _adRepository.Create(ad);

                var adDto = _mapper.Map<AdDto>(ad);
                return new TestsResponse<AdDto>(adDto, "Success");
            }
            catch (Exception ex)
            {
                return new TestsResponse<AdDto>(null, ex.Message);
            }
            
        }

        public async Task<TestsResponse<bool>> Delete(int id)
        {
            try
            {
                var ad = await _adRepository.GetById(id);

                if (ad == null)
                {
                    return new TestsResponse<bool>(false, "ad not found.");
                }
                await _adRepository.Delete(ad);

                return new TestsResponse<bool>(true, "Success.");
            }
            catch (Exception ex)
            {
                return new TestsResponse<bool>(false , ex.Message);
            }
        }

        public async Task<TestsResponse<AdDto>> Edit(int id, AdDto model)
        {
            try
            {
                var ad = await _adRepository.GetById(id);

                if (ad == null)
                {
                    return new TestsResponse<AdDto>(null, "Ad not found.");
                }

                _mapper.Map(model, ad);

                ad.CreatedDate = DateTime.UtcNow;

                await _adRepository.Update(ad);

                var adDto = _mapper.Map<AdDto>(ad);

                return new TestsResponse<AdDto>(adDto, "Success");
            }
            catch (Exception ex)
            {
                return new TestsResponse<AdDto>(null, ex.Message);
            }
        }

        public async Task<TestsResponse<AdDto>> Get(int id)
        {
            try
            {
                var ad = await _adRepository.GetById(id);

                if (ad == null)
                {
                    return new TestsResponse<AdDto>(null, "Ad not found.");
                }

                var adDto = _mapper.Map<AdDto>(ad);
                
                return new TestsResponse<AdDto>(adDto, "Success.");
            }
            catch (Exception ex)
            {
                return new TestsResponse<AdDto>(null , ex.Message);
            }
        }

        public async Task<TestsResponse<IEnumerable<AdDto>>> GetAll()
        {
            try
            {
                var ads = await _adRepository.GetAll();

                if (ads.Count() == 0)
                {
                    return new TestsResponse<IEnumerable<AdDto>>(null, "0 elements.");
                }

                //_mapper.Map<IEnumerable<TicketDto>>(tickets);
                var adDtos = _mapper.Map<IEnumerable<AdDto>>(ads);
                
                return new TestsResponse<IEnumerable<AdDto>>(adDtos, "Success.");
            }
            catch (Exception ex)
            {
                return new TestsResponse<IEnumerable<AdDto>>(null, ex.Message);
            }
        }
    }
}

using AutoMapper;
using Dorm.BLL.Interfaces;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Entities.Laundry;
using Dorm.Domain.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.BLL.Services
{
    public class WasherService : IWasherService
    {
        private readonly IWasherRepository _washerRepository;
        private readonly IMapper _mapper;
        public WasherService(IWasherRepository washerRepository, IMapper mapper)
        {
            _washerRepository = washerRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponse<ReservationDto>> Create(ReservationDto reservationDto)
        {
            try
            {
                var reservation = _mapper.Map<Reservation>(reservationDto);
                await _washerRepository.Create(reservation);
                return new BaseResponse<ReservationDto>(_mapper.Map<ReservationDto>(reservation), "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<ReservationDto>(null, ex.Message);
            }
        }

        public async Task<BaseResponse<bool>> Delete(int reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IEnumerable<ReservationDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IEnumerable<ReservationDto>>> GetAllByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IEnumerable<ReservationDto>>> GetAllByWasherId(int washerId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<ReservationDto>> GetById(int reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasReservation(int washerId, DateTime startTime, DateTime endTime)
        {
            var reservation = await _washerRepository.HasReservation(washerId, startTime, endTime);
            return reservation.Any();
        }

        public async Task<BaseResponse<ReservationDto>> Update(int reservationId, ReservationDto reservationDto)
        {
            throw new NotImplementedException();
        }
    }
}

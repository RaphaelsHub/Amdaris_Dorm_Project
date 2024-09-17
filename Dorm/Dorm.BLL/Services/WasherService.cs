using AutoMapper;
using Dorm.BLL.Interfaces;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Entities.Laundry;
using Dorm.Domain.Responces;

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
            try
            {
                var reservation = await _washerRepository.GetById(reservationId);
                if (reservation == null)
                    return new BaseResponse<bool>(false, $"Reservation with ID {reservationId} not found.");
                await _washerRepository.Delete(reservation);
                return new BaseResponse<bool>(true, "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<ReservationDto>>> GetAll()
        {
            try
            {
                var reservations = await _washerRepository.GetAll();
                if (!reservations.Any())
                {
                    return new BaseResponse<IEnumerable<ReservationDto>>(null, "Reservations not found.");
                }
                return new BaseResponse<IEnumerable<ReservationDto>>(_mapper.Map<IEnumerable<ReservationDto>>(reservations), "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ReservationDto>> (null, ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<ReservationDto>>> GetAllByUserId(int userId)
        {
            try
            {
                var reservations = await _washerRepository.GetAllByUserId(userId);
                if (!reservations.Any())
                {
                    return new BaseResponse<IEnumerable<ReservationDto>>(null, "Reservations not found.");
                }
                return new BaseResponse<IEnumerable<ReservationDto>>(_mapper.Map<IEnumerable<ReservationDto>>(reservations), "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ReservationDto>>(null, ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<ReservationDto>>> GetAllByWasherId(int washerId)
        {
            try
            {
                var reservations = await _washerRepository.GetAllByWasherId(washerId);
                if (!reservations.Any())
                {
                    return new BaseResponse<IEnumerable<ReservationDto>>(null, "Reservations not found.");
                }
                return new BaseResponse<IEnumerable<ReservationDto>>(_mapper.Map<IEnumerable<ReservationDto>>(reservations), "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ReservationDto>>(null, ex.Message);
            }
        }

        public async Task<BaseResponse<ReservationDto>> GetById(int reservationId)
        {
            try
            {
                var reservation = await _washerRepository.GetById(reservationId);
                if (reservation == null)
                    return new BaseResponse<ReservationDto>(null, $"Ticket with ID {reservationId} not found.");

                return new BaseResponse<ReservationDto>(_mapper.Map<ReservationDto>(reservation), "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<ReservationDto>(null, ex.Message);
            }
        }

        public async Task<BaseResponse<bool>> HasReservation(int washerId, DateTime startTime, DateTime endTime)
        {
            try
            {
                var reservations = await _washerRepository.HasReservation(washerId, startTime, endTime);
                return new BaseResponse<bool>(reservations.Any(), "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<ReservationDto>> Update(int reservationId, ReservationDto reservationDto)
        {
            throw new NotImplementedException();
        }
    }
}

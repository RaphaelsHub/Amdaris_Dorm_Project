using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;

namespace Dorm.BLL.Interfaces
{
    public interface IWasherService
    {
        Task<BaseResponse<ReservationDto>> GetById(int reservationId);
        Task<BaseResponse<IEnumerable<ReservationDto>>> GetAll();
        Task<BaseResponse<IEnumerable<ReservationDto>>> GetAllByUserId(int userId);
        Task<BaseResponse<IEnumerable<ReservationDto>>> GetAllByWasherId(int washerId);
        Task<BaseResponse<ReservationDto>> Create(ReservationDto reservationDto);
        Task<BaseResponse<bool>> Delete(int reservationId);
        Task<BaseResponse<ReservationDto>> Update(int reservationId, ReservationDto reservationDto);
        Task<BaseResponse<bool>> HasReservation(int washerId, DateTime startTime, DateTime endTime);
    }
}

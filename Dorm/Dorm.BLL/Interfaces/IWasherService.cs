using Dorm.Domain.DTO;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Task<bool> HasReservation(int washerId, DateTime startTime, DateTime endTime);
    }
}

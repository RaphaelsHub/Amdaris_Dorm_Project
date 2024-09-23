using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Entities.Laundry;
using Dorm.Domain.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.DAL.Interfaces
{
    public interface IWasherRepository : IBaseRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> HasReservation(int washerId, DateTime startTime, DateTime endTime);
        Task<IEnumerable<Reservation>> GetAllByUserId(int userId);
        Task<IEnumerable<Reservation>> GetAllByWasherId(int washerId);
    }
}

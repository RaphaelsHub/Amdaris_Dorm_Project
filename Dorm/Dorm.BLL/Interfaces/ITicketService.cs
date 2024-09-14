using Dorm.Domain.DTO;
using Dorm.Domain.Responces;

namespace Dorm.BLL.Interfaces
{
    public interface ITicketService
    {
        Task<BaseResponse<TicketDto>> GetById(int ticketId);
        Task<BaseResponse<IEnumerable<TicketDto>>> GetAll();
        Task<BaseResponse<TicketDto>> Create(TicketDto ticketDto);
        Task<BaseResponse<bool>> Delete(int ticketId);
        Task<BaseResponse<TicketDto>> Update(int ticketId, TicketDto ticketDto);
    }
}

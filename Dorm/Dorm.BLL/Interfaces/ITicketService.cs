using Dorm.Domain.DTO;
using Dorm.Domain.Responces;

namespace Dorm.BLL.Interfaces
{
    public interface ITicketService
    {
        Task<TestsResponse<TicketDto>> GetById(int ticketId);
        Task<TestsResponse<IEnumerable<TicketDto>>> GetAll();
        Task<TestsResponse<TicketDto>> Create(TicketDto ticketDto);
        Task<TestsResponse<bool>> Delete(int ticketId);
        Task<TestsResponse<TicketDto>> Update(int ticketId, TicketDto ticketDto);
        Task<TestsResponse<TicketDto>> AddResponse(int ticketId, TicketDto ticketDto);
    }
}

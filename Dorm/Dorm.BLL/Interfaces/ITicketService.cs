using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.BLL.Interfaces
{
    public interface ITicketService
    {
        Task<TicketDto?> GetById(int ticketId);
        Task<IEnumerable<TicketDto>> GetAll();
        Task<TicketDto> Create(TicketDto ticketDto);
        Task<bool> Delete(int ticketId);
        Task<TicketDto> Update(int ticketId, TicketDto ticketDto);
    }
}

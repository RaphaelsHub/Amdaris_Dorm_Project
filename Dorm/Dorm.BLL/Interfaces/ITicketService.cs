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
        Task<TicketDto?> GetTicketById(int ticketId);
        Task<TicketDto> CreateTicket(TicketDto ticketDto);
        Task<bool> DeleteTicket(int ticketId);
        Task<TicketDto> UpdateTicket(int ticketId, TicketDto ticketDto);
    }
}

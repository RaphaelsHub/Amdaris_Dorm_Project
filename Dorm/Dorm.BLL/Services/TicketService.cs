using Dorm.BLL.Interfaces;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.BLL.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Ticket> CreateTicket(TicketDto ticketDto)
        {
            Ticket ticket = new()
            {
                Id = ticketDto.Id,
                Name = ticketDto.Name,
                Group = ticketDto.Group,
                Room = ticketDto.Room,
                Type = ticketDto.Type,
                Subject = ticketDto.Subject,
                Description = ticketDto.Description,
                RespondentId = ticketDto.Respondent.Id,
                Date = ticketDto.Date,
                Response = ticketDto.Response,
                Status = Domain.Enums.TicketStatus.SENT
            };
            await _ticketRepository.Create(ticket);
            return ticket;
        }

        public async Task<bool> DeleteTicket(int ticketId)
        {
            var ticket = await GetTicketById(ticketId);
            if (ticket != null)
                return await _ticketRepository.Delete(ticket);
            return false;
        }

        public async Task<Ticket?> GetTicketById(int ticketId)
        {
            return await _ticketRepository.GetById(ticketId);
        }
    }
}

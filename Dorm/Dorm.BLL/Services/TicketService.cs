using AutoMapper;
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
        private readonly IMapper _mapper;
        public TicketService(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<TicketDto> Create(TicketDto ticketDto)
        {
            Ticket ticket = _mapper.Map<Ticket>(ticketDto);
            await _ticketRepository.Create(_mapper.Map<Ticket>(ticketDto));
            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task<bool> Delete(int ticketId)
        {
            var ticket = await _ticketRepository.GetById(ticketId);
            if (ticket != null)
                return await _ticketRepository.Delete(ticket);
            return false;
        }

        public async Task<IEnumerable<TicketDto>> GetAll()
        {
            var tickets = await _ticketRepository.GetAll();
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public async Task<TicketDto?> GetById(int ticketId)
        {
            return _mapper.Map<TicketDto?>(await _ticketRepository.GetById(ticketId));
        }

        public async Task<TicketDto> Update(int ticketId, TicketDto ticketDto)
        {
            var ticket = await _ticketRepository.GetById(ticketId) ?? throw new KeyNotFoundException($"Ticket with ID {ticketId} not found.");
            _mapper.Map(ticketDto, ticket);
            await _ticketRepository.Update(ticket);
            return _mapper.Map<TicketDto>(ticket);
        }
    }
}

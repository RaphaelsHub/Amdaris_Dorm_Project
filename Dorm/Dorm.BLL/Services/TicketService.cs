using AutoMapper;
using Dorm.BLL.Interfaces;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ticket;
using Dorm.Domain.Responces;

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

        public async Task<TestsResponse<TicketDto>> AddResponse(int ticketId, TicketDto ticketDto)
        {
            try
            {
                var ticket = await _ticketRepository.GetById(ticketId);
                if (ticket == null)
                    return new TestsResponse<TicketDto>(null, $"Ticket with ID {ticketId} not found.");

                ticket.RespondentId = ticketDto.RespondentId;
                ticket.RespondentName = ticketDto.RespondentName;
                ticket.RespondentEmail = ticketDto.RespondentEmail;
                ticket.Response = ticketDto.Response;
                ticket.Status = Domain.Enum.Ticket.TicketStatus.IN_PROCESS;

                await _ticketRepository.Update(ticket);
                return new TestsResponse<TicketDto>(_mapper.Map<TicketDto>(ticket), "Success.");
            }
            catch (Exception ex)
            {
                return new TestsResponse<TicketDto>(null, ex.Message);
            }
        }

        public async Task<TestsResponse<TicketDto>> Create(TicketDto ticketDto)
        {
            try
            {
                var ticket = _mapper.Map<Ticket>(ticketDto);
                await _ticketRepository.Create(ticket);
                return new TestsResponse<TicketDto>(_mapper.Map<TicketDto>(ticket), "Success.");
            }
            catch (Exception ex)
            {
                return new TestsResponse<TicketDto>(null, ex.Message);
            }
        }

        public async Task<TestsResponse<bool>> Delete(int ticketId)
        {
            try
            {
                var ticket = await _ticketRepository.GetById(ticketId);
                if (ticket == null)
                    return new TestsResponse<bool>(false, $"Ticket with ID {ticketId} not found.");
                await _ticketRepository.Delete(ticket);
                return new TestsResponse<bool>(true, "Success.");
            }
            catch (Exception ex)
            {
                return new TestsResponse<bool>(false, ex.Message);
            }
        }

        public async Task<TestsResponse<IEnumerable<TicketDto>>> GetAll()
        {
            try
            {
                var tickets = await _ticketRepository.GetAll();
                if (!tickets.Any())
                {
                    return new TestsResponse<IEnumerable<TicketDto>> (null, "Tickets not found.");
                }
                return new TestsResponse<IEnumerable<TicketDto>>(_mapper.Map<IEnumerable<TicketDto>>(tickets), "Success.");
            }
            catch (Exception ex)
            {
                return new TestsResponse<IEnumerable<TicketDto>>(null, ex.Message);
            }
        }

        public async Task<TestsResponse<TicketDto>> GetById(int ticketId)
        {
            try
            {
                var ticket = await _ticketRepository.GetById(ticketId);
                if(ticket == null)
                    return new TestsResponse<TicketDto>(null, $"Ticket with ID {ticketId} not found.");

                return new TestsResponse<TicketDto>(_mapper.Map<TicketDto>(ticket), "Success.");
            }
            catch (Exception ex)
            {
                return new TestsResponse<TicketDto>(null, ex.Message);
            }
        }

        public async Task<TestsResponse<TicketDto>> Update(int ticketId, TicketDto ticketDto)
        {
            try
            {
                var ticket = await _ticketRepository.GetById(ticketId);
                if (ticket == null)
                    return new TestsResponse<TicketDto>(null, $"Ticket with ID {ticketId} not found.");

                //int temp = ticketDto.UserId;
                _mapper.Map(ticketDto, ticket);
                //ticket.UserId = temp;
                await _ticketRepository.Update(ticket);
                return new TestsResponse<TicketDto>(_mapper.Map<TicketDto>(ticket), "Success.");
            }
            catch (Exception ex)
            {
                return new TestsResponse<TicketDto>(null, ex.Message);
            }
        }
    }
}

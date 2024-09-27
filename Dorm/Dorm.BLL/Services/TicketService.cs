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

        public async Task<BaseResponse<TicketDto>> AddResponse(int ticketId, TicketDto ticketDto)
        {
            try
            {
                var ticket = await _ticketRepository.GetById(ticketId);
                if (ticket == null)
                    return new BaseResponse<TicketDto>(null, $"Ticket with ID {ticketId} not found.");

                ticket.RespondentId = ticketDto.RespondentId;
                ticket.RespondentName = ticketDto.RespondentName;
                ticket.RespondentEmail = ticketDto.RespondentEmail;
                ticket.Response = ticketDto.Response;
                ticket.Status = Domain.Enum.Ticket.TicketStatus.IN_PROCESS;

                await _ticketRepository.Update(ticket);
                return new BaseResponse<TicketDto>(_mapper.Map<TicketDto>(ticket), "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<TicketDto>(null, ex.Message);
            }
        }

        public async Task<BaseResponse<TicketDto>> Create(TicketDto ticketDto)
        {
            try
            {
                var ticket = _mapper.Map<Ticket>(ticketDto);
                await _ticketRepository.Create(ticket);
                return new BaseResponse<TicketDto>(_mapper.Map<TicketDto>(ticket), "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<TicketDto>(null, ex.Message);
            }
        }

        public async Task<BaseResponse<bool>> Delete(int ticketId)
        {
            try
            {
                var ticket = await _ticketRepository.GetById(ticketId);
                if (ticket == null)
                    return new BaseResponse<bool>(false, $"Ticket with ID {ticketId} not found.");
                await _ticketRepository.Delete(ticket);
                return new BaseResponse<bool>(true, "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<TicketDto>>> GetAll()
        {
            try
            {
                var tickets = await _ticketRepository.GetAll();
                if (!tickets.Any())
                {
                    return new BaseResponse<IEnumerable<TicketDto>> (null, "Tickets not found.");
                }
                return new BaseResponse<IEnumerable<TicketDto>>(_mapper.Map<IEnumerable<TicketDto>>(tickets), "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TicketDto>>(null, ex.Message);
            }
        }

        public async Task<BaseResponse<TicketDto>> GetById(int ticketId)
        {
            try
            {
                var ticket = await _ticketRepository.GetById(ticketId);
                if(ticket == null)
                    return new BaseResponse<TicketDto>(null, $"Ticket with ID {ticketId} not found.");

                return new BaseResponse<TicketDto>(_mapper.Map<TicketDto>(ticket), "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<TicketDto>(null, ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<TicketDto>>> GetByUserId(int userId)
        {
            try
            {
                var tickets = await _ticketRepository.GetByUserId(userId);
                if (!tickets.Any())
                {
                    return new BaseResponse<IEnumerable<TicketDto>>(null, "Tickets not found.");
                }
                return new BaseResponse<IEnumerable<TicketDto>>(_mapper.Map<IEnumerable<TicketDto>>(tickets), "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TicketDto>>(null, ex.Message);
            }
        }

        public async Task<BaseResponse<TicketDto>> Update(int ticketId, TicketDto ticketDto)
        {
            try
            {
                var ticket = await _ticketRepository.GetById(ticketId);
                if (ticket == null)
                    return new BaseResponse<TicketDto>(null, $"Ticket with ID {ticketId} not found.");

                //int temp = ticketDto.UserId;
                _mapper.Map(ticketDto, ticket);
                //ticket.UserId = temp;
                await _ticketRepository.Update(ticket);
                return new BaseResponse<TicketDto>(_mapper.Map<TicketDto>(ticket), "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<TicketDto>(null, ex.Message);
            }
        }
    }
}

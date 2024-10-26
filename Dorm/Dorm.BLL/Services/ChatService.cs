using AutoMapper;
using Dorm.BLL.Interfaces;
using Dorm.DAL.Interfaces;
using Dorm.DAL.Repositories;
using Dorm.Domain.DTO;
using Dorm.Domain.DTO.Chat;
using Dorm.Domain.Entities.Ad;
using Dorm.Domain.Entities.Chat;
using Dorm.Domain.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.BLL.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;
        public ChatService(IChatRepository chatRepository, IMapper mapper)
        {
            _mapper = mapper;
            _chatRepository = chatRepository;
        }

        public async Task<BaseResponse<bool>> Create(ChatDto model)
        {
            try
            {
                var chat = _mapper.Map<Chat>(model);

                await _chatRepository.Create(chat);

                return new BaseResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<bool>> Delete(int id)
        {
            try
            {
                var chat = await _chatRepository.GetById(id);

                if (chat == null)
                {
                    return new BaseResponse<bool>(false, "chat not found.");
                }
                await _chatRepository.Delete(chat);

                return new BaseResponse<bool>(true, "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<bool>> DeleteMessage(int id)
        {
            try
            {
                var message = await _chatRepository.GetMessageById(id);

                if (message == null)
                {
                    return new BaseResponse<bool>(false, "message not found.");
                }
                await _chatRepository.DeleteMessage(message);

                return new BaseResponse<bool>(true, "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<bool>> EditMessage(int id, ChatMessageDto model)
        {
            try
            {
                var message = await _chatRepository.GetMessageById(id);

                if (message == null)
                {
                    return new BaseResponse<bool>(true, "message not found.");
                }

                _mapper.Map(model, message);

                await _chatRepository.UpdateMessage(message);

                return new BaseResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>(false, ex.Message);
            }
        }

        public async Task<BaseResponse<ChatDto>> Get(int id)
        {
            try
            {
                var chat = await _chatRepository.GetById(id);

                if (chat == null)
                {
                    return new BaseResponse<ChatDto>(null, "chat not found.");
                }

                var chatDto = _mapper.Map<ChatDto>(chat);

                return new BaseResponse<ChatDto>(chatDto, "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<ChatDto>(null, ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<ChatDto>>> GetAll()
        {
            try
            {
                var chats = await _chatRepository.GetAll();

                if (chats.Count() == 0)
                {
                    return new BaseResponse<IEnumerable<ChatDto>>(null, "0 elements.");
                }

                var chatDtos = _mapper.Map<IEnumerable<ChatDto>>(chats);

                return new BaseResponse<IEnumerable<ChatDto>>(chatDtos, "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ChatDto>>(null, ex.Message);
            }
        }

        // id - ChatID
        public async Task<BaseResponse<IEnumerable<ChatMessageDto>>> GetAllMessages(int id)
        {
            try
            {
                var messages = await _chatRepository.GetAllMessages(id);

                if (messages.Count() == 0)
                {
                    return new BaseResponse<IEnumerable<ChatMessageDto>>(null, "0 elements.");
                }

                var messagesDto = _mapper.Map<IEnumerable<ChatMessageDto>>(messages);

                return new BaseResponse<IEnumerable<ChatMessageDto>>(messagesDto, "Success.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ChatMessageDto>>(null, ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<ChatDto>>> GetAllСhatsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<bool>> SaveMessage(ChatMessageDto entity)
        {
            try
            {
                var message = _mapper.Map<ChatMessage>(entity);
                message.Timestamp = DateTime.UtcNow;

                await _chatRepository.SaveMessage(message);

                return new BaseResponse<bool>(true, "Success");
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>(false, ex.Message);
            }
        }
    }
}

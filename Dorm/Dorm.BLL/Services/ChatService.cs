using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO.Chat;
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
        public Task<BaseResponse<bool>> Create(ChatDto model)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> DeleteMessage(ChatMessageDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> Edit(int id, ChatDto model)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<ChatDto>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<ChatDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<ChatMessageDto>>> GetAllMessages(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<ChatDto>>> GetAllСhatsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> SaveMessage(ChatMessageDto entity)
        {
            throw new NotImplementedException();
        }
    }
}

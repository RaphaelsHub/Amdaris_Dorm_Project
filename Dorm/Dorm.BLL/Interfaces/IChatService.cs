using Dorm.Domain.DTO;
using Dorm.Domain.DTO.Chat;
using Dorm.Domain.Entities.Chat;
using Dorm.Domain.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.BLL.Interfaces
{
    public interface IChatService
    {
        Task<BaseResponse<bool>> Create(ChatDto model);
        Task<BaseResponse<bool>> EditMessage(int id, ChatMessageDto model);
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<ChatDto>> Get(int id);
        Task<BaseResponse<IEnumerable<ChatDto>>> GetAll();
        Task<BaseResponse<IEnumerable<ChatDto>>> GetAllСhatsByUserId(int id);
        Task<BaseResponse<IEnumerable<ChatMessageDto>>> GetAllMessages(int id);
        Task<BaseResponse<bool>> SaveMessage(ChatMessageDto entity);
        Task<BaseResponse<bool>> DeleteMessage(int id);
        //Task<BaseResponse<bool>> DeleteMessage(ChatMessageDto entity);
        //Task<BaseResponse<bool>> AddUserToChat(int chatId, int userId);
    }
}

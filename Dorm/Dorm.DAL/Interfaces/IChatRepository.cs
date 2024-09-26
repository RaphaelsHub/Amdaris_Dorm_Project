using Dorm.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.DAL.Interfaces
{
    public interface IChatRepository : IBaseRepository<Chat>
    {
        Task<IEnumerable<Chat>> GetAllСhatsByUserId(int id);
        Task<IEnumerable<ChatMessage>> GetAllMessages(int id);
        Task<bool> SaveMessage(ChatMessage entity);
        Task<bool> DeleteMessage(ChatMessage entity);
        Task<ChatMessage> GetMessageById(int id);
        Task<ChatMessage> UpdateMessage(ChatMessage entity);
    }
}

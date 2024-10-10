using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.Domain.DTO.Chat
{
    public class ChatMessageDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string UserInfo { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

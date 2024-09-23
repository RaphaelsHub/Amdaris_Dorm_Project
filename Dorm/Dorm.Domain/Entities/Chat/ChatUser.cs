using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.Domain.Entities.Chat
{
    public class ChatUser
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string UserId { get; set; }
    }
}

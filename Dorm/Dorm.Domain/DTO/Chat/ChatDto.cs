using Dorm.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.Domain.DTO.Chat
{
    public class ChatDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public virtual ICollection<ChatUser> Participants { get; set; }
    }
}

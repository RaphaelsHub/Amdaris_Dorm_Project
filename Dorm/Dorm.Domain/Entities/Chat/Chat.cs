using Dorm.Domain.DTO.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.Domain.Entities.Chat
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public virtual ICollection<ChatUser> Participants {  get; set; }
    }
}

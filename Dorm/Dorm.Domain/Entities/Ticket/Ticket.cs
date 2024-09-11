using Dorm.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.Domain.Entities.Ticket
{
    public class Ticket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Room { get; set; }
        public TicketType Type { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        //public User.User Respondent {  get; set; }
        public DateTime Date {  get; set; }
    }
}

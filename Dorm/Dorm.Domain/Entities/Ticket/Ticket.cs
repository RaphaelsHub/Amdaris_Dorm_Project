using Dorm.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.Domain.Entities.Ticket
{
    public class Ticket
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RespondentId {  get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Room { get; set; }
        public TicketType Type { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime Date {  get; set; }
        public string? Response { get; set; }
    }
}

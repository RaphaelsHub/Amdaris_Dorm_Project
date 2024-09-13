using Dorm.Domain.Enum.Ticket;

namespace Dorm.Domain.DTO
{
    public class TicketDto
    {
        public string? Name { get; set; }
        public string? Group { get; set; }
        public string? Room { get; set; }
        public TicketType Type { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public TicketStatus Status { get; set; }
        public UserProfileDto? Respondent { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Response { get; set; }
    }
}

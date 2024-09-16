using Dorm.Domain.Enum.Ticket;

namespace Dorm.Domain.DTO
{
    public class TicketDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Group { get; set; }
        public string? Room { get; set; }
        public TicketType Type { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public TicketStatus Status { get; set; }
        public int RespondentId { get; set; }
        public string? RespondentName { get; set; }
        public string? RespondentEmail { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Response { get; set; }
        public bool canEdit { get; set; }
    }
}

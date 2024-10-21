namespace Dorm.Domain.DTO.Laundry
{
    public class ReservationDto
    {
        public int WasherId { get; set; }
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

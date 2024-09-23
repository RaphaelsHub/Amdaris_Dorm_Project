using Dorm.Domain.Enum.User;

namespace Dorm.Domain.Entities.UserEF
{
    public class UserEF
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public byte[]? Photo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public GenderType Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public string? Faculty { get; set; }
        public string? Specialty { get; set; }
        public string? Group { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? DormitoryAddress { get; set; }
        public string? RoomNumber { get; set; }
        public DateTime StartDateRent { get; set; }
        public DateTime EndDateOfRent { get; set; }
        public byte[]? ContractPhoto { get; set; }
        public decimal MonthlyRent { get; set; }
        public int PaidMonths { get; set; } //оплачено месяцев
        
        public string? PasswordHash { get; set; }
        public string? IpAddress { get; set; }
        public string? DeviceType { get; set; }

    }
}

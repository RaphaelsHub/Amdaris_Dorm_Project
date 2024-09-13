using Dorm.Domain.Enum.User;

namespace Dorm.Domain.Entities.UserEF
{
    public class UserEF
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public UserStatus UserStatus { get; set; } = UserStatus.None;
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? Group { get; set; }
        public string? IpAddress { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? DeviceType { get; set; }
    }
}

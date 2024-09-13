using System.ComponentModel.DataAnnotations;

namespace Dorm.Domain.DTO.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password field is required.")]
        public string? Password { get; set; }

        //for 2fa
        [Required(ErrorMessage = "IpAddress field is required.")]
        public string? IpAddress { get; set; }

        [Required(ErrorMessage = "Country field is required.")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "City field is required.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "DeviceType field is required.")]
        public string? DeviceType { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}

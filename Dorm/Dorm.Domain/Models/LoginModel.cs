using System.ComponentModel.DataAnnotations;

namespace Dorm.Domain.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password field is required.")]
        public string Password { get; set; } = string.Empty;

        //for 2fa
        [Required(ErrorMessage = "IpAddress field is required.")]
        public string IpAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country field is required.")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "City field is required.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "DeviceType field is required.")]
        public string DeviceType { get; set; } = string.Empty;

        public bool RememberMe { get; set; } = false;
    }
}

using System.ComponentModel.DataAnnotations;

namespace Dorm.Domain.DTO.Auth
{
    public class RegistrationDto : LoginDto
    {
        [Required(ErrorMessage = "First name field is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Second name field is required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Group name must be select.")]
        public string? Group { get; set; }

        [Required(ErrorMessage = "The Confirm Password field is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}

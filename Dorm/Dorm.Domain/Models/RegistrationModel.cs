using System.ComponentModel.DataAnnotations;

namespace Dorm.Domain.Models
{
    public class RegistrationModel : LoginModel
    {
        [Required(ErrorMessage = "First name field is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Second name field is required.")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Group name must be select.")]
        public string Group { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Confirm Password field is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

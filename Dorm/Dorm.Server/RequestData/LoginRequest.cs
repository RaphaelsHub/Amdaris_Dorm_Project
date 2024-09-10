using System.ComponentModel.DataAnnotations;

namespace Dorm.Server.RequestData
{
    public class LoginRequest : IValidatableObject
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


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            //Check for lenght parament and expression value
            if (Password.Length < 8 || !System.Text.RegularExpressions.Regex.IsMatch(Password, @"^[A-Za-z\d@$!%*?&]+$"))
            {
                results.Add(new ValidationResult("Password must contain at least 6 English letters(1 uppercase letter), 1 digit, and 1 special characters.",
                    new[] { nameof(Password) }));
            }

            //Check for uppercase letters
            if (!System.Text.RegularExpressions.Regex.IsMatch(Password, @"[A-Z]"))
            {
                results.Add(new ValidationResult("Password must contain at least 1 uppercase letter.", new[] { nameof(Password) }));
            }

            //Check for digits
            if (!System.Text.RegularExpressions.Regex.IsMatch(Password, @"\d"))
            {
                results.Add(new ValidationResult("Password must contain at least 1 digit.", new[] { nameof(Password) }));
            }

            //Check for having special chacters
            if (!System.Text.RegularExpressions.Regex.IsMatch(Password, @"[@$!%*?&]"))
            {
                results.Add(new ValidationResult("Password must contain at least 1 special character.", new[] { nameof(Password) }));
            }

            return new List<ValidationResult>();
        }
    }
}

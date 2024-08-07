using System.ComponentModel.DataAnnotations;

namespace TimeTracking.App.Person.Domain.Model
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Email must be between 1 and 255 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Password must be between 1 and 255 characters")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Confirm Password must be between 1 and 255 characters")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 255 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 255 characters")]
        public string? LastName { get; set; }
    }

}

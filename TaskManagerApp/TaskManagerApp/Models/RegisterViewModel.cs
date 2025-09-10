using System.ComponentModel.DataAnnotations;

namespace TaskManagerApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Full name is required")]
        public string ?  FullName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string ? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string ? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [DataType(DataType.Password)]
        public string  ? Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ? ConfirmPassword { get; set; }
    }
}

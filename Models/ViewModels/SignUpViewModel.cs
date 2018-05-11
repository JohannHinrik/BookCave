using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First Name field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address field is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City field is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country field is required")]
        public string Country { get; set; }
        
        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; }
    }
}
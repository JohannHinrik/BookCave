using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password is required")]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class LogInViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
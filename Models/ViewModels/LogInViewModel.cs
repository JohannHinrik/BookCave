using System.ComponentModel.DataAnnotations;

namespace BookCave.Models
{
    public class LogInViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
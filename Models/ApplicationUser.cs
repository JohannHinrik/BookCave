using Microsoft.AspNetCore.Identity;

namespace BookCave.Models
{
    public class ApplicationUser : IdentityUser
    {
        //Adding properties to a user: 

        public string FirstName {get; set; }
        public string LastName {get; set; }
        public string FavoriteBook {get; set; }

        // Without Arnar: 
        public string City {get; set; }
        public string Country {get; set; }
        public string Address {get; set; }
        public string Image {get; set; }
    }
}
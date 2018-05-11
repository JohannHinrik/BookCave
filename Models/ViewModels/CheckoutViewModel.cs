namespace BookCave.Models.ViewModels
{
    public class CheckoutViewModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int CardNumber { get; set; }
        public int Cvc { get; set; }
        public int ExpiryYear { get; set; }
        public int ExpiryMonth { get; set; }
        public int ZipCode { get; set; }
    }
}
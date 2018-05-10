namespace BookCave.Models.ViewModels
{
    public class CreditCardViewModel
    {
        public int CardId { get; set; }
        public int CardNumber { get; set; }
        public int Cvc { get; set; }
        public int ExpiryYear { get; set; }
        public int ExpiryMonth { get; set; }
    }
}
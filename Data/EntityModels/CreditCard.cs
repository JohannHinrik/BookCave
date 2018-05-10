namespace BookCave.Data.EntityModels
{
    public class CreditCard
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CardNumber { get; set; }
        public int Cvc { get; set; }
        public int ExpiryYear { get; set; }
        public int ExpiryMonth { get; set; }
    }
}
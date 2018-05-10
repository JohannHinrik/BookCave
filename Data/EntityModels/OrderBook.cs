namespace BookCave.Data.EntityModels
{
    public class OrderBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
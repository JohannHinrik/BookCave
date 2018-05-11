namespace BookCave.Data.EntityModels
{
    public class Wishlist
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
    }
}
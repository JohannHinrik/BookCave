namespace BookCave.Data.EntityModels
{
    public class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int AccountId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

    }
}
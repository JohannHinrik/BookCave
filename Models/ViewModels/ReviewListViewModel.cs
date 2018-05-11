namespace BookCave.Models.ViewModels
{
    public class ReviewListViewModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string AccountId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
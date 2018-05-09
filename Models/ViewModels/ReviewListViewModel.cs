namespace BookCave.Models.ViewModels
{
    public class ReviewListViewModel
    {
        public int BookId { get; set; }
        public int AccountId { get; set; }
        public string Comment { get; set; }
        public int Id { get; set; }
        public int Rating { get; set; }

    }
}
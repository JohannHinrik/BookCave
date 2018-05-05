namespace BookCave.Model.ViewModels
{
    public class BookListViewModel
    {
        public int Isbn { get; set; }
        public int AuthorId { get; set; }
        public int ReviewId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string About { get; set; }
        public double Rating { get; set; }
    }
}
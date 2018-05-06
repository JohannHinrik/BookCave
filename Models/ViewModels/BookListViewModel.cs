namespace BookCave.Models.ViewModels
{
    public class BookListViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Review { get; set; }
        public string Genre { get; set; }
        public string About { get; set; }
        public double Rating { get; set; }
        public int Price { get; set; }
    }
}
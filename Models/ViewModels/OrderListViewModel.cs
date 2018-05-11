using BookCave.Data.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class OrderListViewModel
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
        public int Book { get; set; }
        public int User { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
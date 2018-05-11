using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class CartViewModel
    {
        public List<BookListViewModel> Books { get; set; }
        public string User { get; set; }
    }
}
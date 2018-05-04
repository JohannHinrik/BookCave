using System.Collections.Generic;
using BookCave.Models.ViewModels;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        public List<BookViewModel> GetAllBooks()
        {
            var Books = new List<BookViewModel>
            {
                new BookViewModel { Title = "Prisoner of Azcaban", Author = "J.K. Rowling", Review = "VEry nice", Genre = "Romance", Rating = 10,5, About = "harry potter is the boy who lived, read all about it" },
                new BookViewModel { Title = "Notting hill", Author = "Gudrun Sara", Review = "More nice", Genre = "Romance", Rating = 9,5, About = "bets book ever" }
            };
            return Books;
        }
    }
}
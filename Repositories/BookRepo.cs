using System.Collections.Generic;
using BookCave.Model.ViewModels;
//using BookCave.Models.ViewModels;

namespace BookCave.Repositories
{
    public class BookRepo
    {
       public List<BookListViewModel> GetAllBooks()
        {
          var Books = new List<BookListViewModel>
            {
                new BookListViewModel { Title = "Prisoner of Azcaban", 
                                    Author = "J.K. Rowling", 
                                    Review = "VEry nice", 
                                    Genre = "Romance", 
                                    About = "harry potter is the boy who lived, read all about it", 
                                    Rating = 10.5 }
                                    
               /* new BookViewModel { Title = "Notting hill",
                                    Author = "Gudrun Sara", 
                                    Review = "More nice", 
                                    Genre = "Romance", 
                                    Rating = 9.5, 
                                    About = "bets book ever" }*/
            };
            return Books; 
        }
    }
}

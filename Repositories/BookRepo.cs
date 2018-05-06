using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        private DataContext _db;
        
        public BookRepo()
        {
            _db = new DataContext();
        }

        public List<BookListViewModel> GetAllBooks()
        {
            var books = (from b in _db.Books
                         join au in _db.Authors on b.AuthorId equals au.Id
                         select new BookListViewModel
                         {
                             BookId = b.Id,
                             Title = b.Title,
                             Genre = b.Genre,
                             //ReviewId = b.Id,
                             About = b.About,
                             Rating = b.Rating,
<<<<<<< HEAD
                             Author = au.Name
=======
                             Price = b.Price
>>>>>>> efcd9eb724d26d82641cfa9bca6ed2be6514a443
                         }).ToList();
            return books;
        }

        public List<BookListViewModel> GetTopRatedBooks()
        {
            var topRatedbooks = (from b in _db.Books
                         join au in _db.Authors on b.AuthorId equals au.Id
                         orderby b.Rating
                         select new BookListViewModel
                         {
                             BookId = b.Id,
                             Title = b.Title,
                             Genre = b.Genre,
                             //ReviewId = b.Id,
                             About = b.About,
                             Rating = b.Rating,
<<<<<<< HEAD
                             Author = au.Name
=======
                             Price = b.Price
>>>>>>> efcd9eb724d26d82641cfa9bca6ed2be6514a443
                         }).Take(10).ToList();
            return topRatedbooks;
        }
    }
}
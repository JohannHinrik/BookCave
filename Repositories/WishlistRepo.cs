using System.Linq;
using BookCave.Models.ViewModels;
using System.Collections.Generic;
using BookCave.Data;

namespace BookCave.Repositories
{
    public class WishlistRepo
    {
        private DataContext _db;
        public WishlistRepo()
        {
            _db = new DataContext();
        }

        public List<BookListViewModel> GetBooks(string userId)
        {
            var books = (from b in _db.Books
                         join c in _db.Carts on b.Id equals c.BookId
                         join a in _db.Authors on b.AuthorId equals a.Id
                         where c.UserId == userId && c.Quantity != 0
                         select new BookListViewModel()
                         {
                            BookId = b.Id,
                            Title = b.Title,
                            Author = a.Name,
                            Price = b.Price,
                            Quantity = c.Quantity
                         }).ToList();
            return books;
        }
    }
}
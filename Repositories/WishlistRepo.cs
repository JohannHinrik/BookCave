using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;
using System;
using BookCave.Data.EntityModels;

namespace BookCave.Repositories
{
    public class WishlistRepo
    {
        /* Private variable that connect the Controller to the Repo-Layer */
        private DataContext _db;
        
        /* Constructor: */
        public WishlistRepo()
        {
            _db = new DataContext();
        }

        public List<BookListViewModel> GetBooks(string userId)
        {
            var books = (from b in _db.Books
                         join c in _db.Wishlists on b.Id equals c.BookId
                         join a in _db.Authors on b.AuthorId equals a.Id
                         where c.UserId == userId
                         select new BookListViewModel()
                         {
                            BookId = b.Id,
                            Title = b.Title,
                            Author = a.Name,
                            Price = b.Price
                         }).ToList();
            return books;
        }

         public void AddToWishlist(string userId, int bookId)
        {
            var connection = (from c in _db.Wishlists
                              where c.UserId == userId && c.BookId == bookId
                              select c).FirstOrDefault();
            if(connection == null)
            {
                var newConnection = new Wishlist()
                {
                    UserId = userId,
                    BookId = bookId
                };
                _db.Wishlists.Add(newConnection);
                _db.SaveChanges();
            }
        }
    }
}
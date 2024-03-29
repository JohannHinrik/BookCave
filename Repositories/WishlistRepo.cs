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
        // Private variable that connect the Controller to the database 
        private DataContext _db;

        // Constructor: 
        public WishlistRepo()
        {
            _db = new DataContext();
        }


        // Function that returns a list of books from a users wishlist 
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

        // Function that adds a book to the users wishlist in the database 
        public void AddToWishlist(string userId, int bookId)
        {
            var connection = (from c in _db.Wishlists
                              where c.UserId == userId && c.BookId == bookId
                              select c).FirstOrDefault();
            if (connection == null)
            {
                var newConnection = new Wishlist()
                {
                    UserId = userId,
                    BookId = bookId
                };

                // Add to the database
                _db.Wishlists.Add(newConnection);
                _db.SaveChanges();
            }
        }
    }
}
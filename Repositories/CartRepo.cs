using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;
using System;
using BookCave.Data.EntityModels;

namespace BookCave.Repositories
{
    public class CartRepo
    {
        private DataContext _db;
        public CartRepo()
        {
            _db = new DataContext();
        }

        /* Adding an item to cart */
        public void AddItem(string userId, int bookId)
        {
            var connection = (from c in _db.Carts
                              where c.UserId == userId && c.BookId == bookId
                              select c).FirstOrDefault();
            if(connection != null)
            {
                connection.Quantity++;
                _db.Carts.Update(connection);
                _db.SaveChanges();
            }
            else
            {
                var newConnection = new Cart()
                {
                    UserId = userId,
                    BookId = bookId,
                    Quantity = 1
                };
                _db.Carts.Add(newConnection);
                _db.SaveChanges();
            }
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
        public void DeleteItem(string userId, int bookId)
        {
            var connection = (from c in _db.Carts
                              where c.UserId == userId && c.BookId == bookId
                              select c).FirstOrDefault();
            connection.Quantity = 0;
             _db.Carts.Update(connection);
                _db.SaveChanges();
            }
        /* public void CartUpdate(string userId, int bookId, string amount)
        {
            var connection = (from c in _db.Carts
                              where c.UserId == userId && c.BookId == bookId
                              select c).FirstOrDefault();
            connection.Quantity = amount;
             _db.Carts.Update(connection);
                _db.SaveChanges();
            } */
    }
}
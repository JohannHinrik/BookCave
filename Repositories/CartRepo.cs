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
        /* Private variable that connect the Controller to the Repo-Layer */
        private DataContext _db;
        
        /* Constructor: */
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
                connection.Payed = false;
                _db.Carts.Update(connection);
                _db.SaveChanges();
            }
            else
            {
                var newConnection = new Cart()
                {
                    UserId = userId,
                    BookId = bookId,
                    Quantity = 1,
                    Payed = false
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
                         where c.UserId == userId && c.Quantity != 0 && c.Payed == false
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
        public void UpdateCart(string userId, int bookId, int amount)
        {
            var connection = (from c in _db.Carts
                              where c.UserId == userId && c.BookId == bookId
                              select c).FirstOrDefault();
            connection.Quantity = amount;
             _db.Carts.Update(connection);
                _db.SaveChanges();
        } 

        public List<CartViewModel> getOrderList(string userId, string userName)
        {
            var ListOfOrders = (from c in _db.Carts
                                where c.UserId == userId
                                select new CartViewModel
                                {
                                    Books = (from b in _db.Books
                                            join a in _db.Authors on b.AuthorId equals a.Id
                                            where c.UserId == userId && b.  Id == c.BookId && c.Payed == true
                                            select new BookListViewModel()
                                            {
                                                BookId = b.Id,
                                                Title = b.Title,
                                                Author = a.Name,
                                                Price = b.Price,
                                                Quantity = c.Quantity
                                            }).ToList(),
                                    User = userName
                                }).ToList();

             return ListOfOrders;
        }

        public void UpdateCartPay(string userId)
        {
            var connection = (from c in _db.Carts
                              where c.UserId == userId
                              select c).ToList();
                              
            foreach(var c in connection)
            {
                c.Payed = true;
                c.Quantity = 0;
            }
             _db.Carts.UpdateRange(connection);
                _db.SaveChanges();
        } 

    }
}
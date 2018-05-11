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

        /* Private variable that connect the Controller to the database */
        private DataContext _db;
        
        /* Constructor: */
        public CartRepo()
        {
            _db = new DataContext();
        }


        /* function that adds an item to the users cart */
        public void AddItem(string userId, int bookId)
        {
            // finds the connectopn between cart, user and books
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

                // Adds the new connection the database
                _db.Carts.Add(newConnection);

                // Save the changes to database
                _db.SaveChanges();
            }
        }


        /* Returns the list of books from the users cart */
        public List<BookListViewModel> GetBooks(string userId)
        {
            // Makes a new list of BookListViewModel that holds all the books from the cart
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


        /* function that delets a book from the cart */
        public void DeleteItem(string userId, int bookId)
        {
            // Finds and selects the connection
            var connection = (from c in _db.Carts
                              where c.UserId == userId && c.BookId == bookId
                              select c).FirstOrDefault();

            // Set the quantity variable to zero (to delete it)
            connection.Quantity = 0;

            //Update the database
             _db.Carts.Update(connection);
             _db.SaveChanges();
            }


        /* Function that updates the quantity of a chosen book in the cart*/
        public void UpdateCart(string userId, int bookId, int amount)
        {
            // finds the right connection
            var connection = (from c in _db.Carts
                              where c.UserId == userId && c.BookId == bookId
                              select c).FirstOrDefault();

            // Set the quantity to the amount (taken in as a parameter)                  
            connection.Quantity = amount;

            // Updateing the database
             _db.Carts.Update(connection);
             _db.SaveChanges();
        } 


        /* Function that returns a list of CartViewModels (that keeps a list of all books in a users cart) */
        public List<CartViewModel> getOrderList(string userId, string userName)
        {
            // Gets a list of all CartViewModels connected to the user
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

        /* Function that empties the cart after an order has been payed */
        public void UpdateCartPay(string userId)
        {
            // Gets a list of all the connections connected to the user
            var connection = (from c in _db.Carts
                              where c.UserId == userId
                              select c).ToList();

            // Sets the variable Payed to true 
            // and sets the variable Quantity to zero               
            foreach(var c in connection)
            {
                c.Payed = true;
                c.Quantity = 0;
            }

            // Updates the database
             _db.Carts.UpdateRange(connection);
                _db.SaveChanges();
        } 

    }
}
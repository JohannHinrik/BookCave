using System;
using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class CartService
    {
        /* Private variable that connect the Controller to the Repo-Layer */
        private CartRepo _cartRepo;

        /* Constructor: */
        public CartService()
        {
            _cartRepo = new CartRepo();
        }


        /* Function that adds a book to the database through the repository layer */
        public void AddItem(string userId, int bookId)
        {
            _cartRepo.AddItem(userId, bookId);
        }


        /* Function that returns a list of all books in users cart from the repository layer */
        public List<BookListViewModel> GetBooks(string userId)
        {
            return _cartRepo.GetBooks(userId);
        }


        /* Void function that deletes a chosen item from the users cart  */
        public void DeleteItem(string userId, int bookId)
        {
            _cartRepo.DeleteItem(userId, bookId);
        }


        /* Void function that updates the quantity of a book in cart from the repository layer */
        public void UpdateCart(string userId, int bookId, int amount)
        {
            _cartRepo.UpdateCart(userId, bookId, amount);
        }


        /* Void function that empties the cart after a order has been payed */
        public void UpdateCartPay(string userId)
        {
            _cartRepo.UpdateCartPay(userId);
        }


        /* Function that returns a list of CartViewModels (that keeps a list of all books in a users cart)*/
        public List<CartViewModel> getOrderList(string userId, string userName)
        {
            var ListOfOrders =_cartRepo.getOrderList(userId, userName);
            return ListOfOrders;
        }
    }
}
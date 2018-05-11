using System;
using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class CartService
    {
        private CartRepo _cartRepo;
        public CartService()
        {
            _cartRepo = new CartRepo();
        }

        public void AddItem(string userId, int bookId)
        {
            _cartRepo.AddItem(userId, bookId);
        }

        public List<BookListViewModel> GetBooks(string userId)
        {
            return _cartRepo.GetBooks(userId);
        }
        public void DeleteItem(string userId, int bookId)
        {
            _cartRepo.DeleteItem(userId, bookId);
        }
        public void UpdateCart(string userId, int bookId, int amount)
        {
            _cartRepo.UpdateCart(userId, bookId, amount);
        }

        public void UpdateCartPay(string userId)
        {
            _cartRepo.UpdateCartPay(userId);
        }
        public List<CartViewModel> getOrderList(string userId, string userName)
        {
            var ListOfOrders = _cartRepo.getOrderList(userId, userName);
            return ListOfOrders;
        }
    }
}
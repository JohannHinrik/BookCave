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
    }
}
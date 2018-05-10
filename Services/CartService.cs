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

         public List<OrderListViewModel> GetUserOrder()
        {
            var orders = _cartRepo.GetUserOrder();
            return orders;
        }
    }
}
using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class OrderBookService
    {
        private OrderBookRepo _orderBookRepo;

        public OrderBookService()
        {
            _orderBookRepo = new OrderBookRepo();
        }
    }
}
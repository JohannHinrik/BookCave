using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    // SEE FINAL REPORT FOR EXPLANATION 
    public class OrderBookService
    {
        // Private variable that connect the Controller to the Repo-Layer 
        private OrderBookRepo _orderBookRepo;


        // Constructor: 
        public OrderBookService()
        {
            _orderBookRepo = new OrderBookRepo();
        }
    }
}
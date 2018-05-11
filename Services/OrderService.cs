using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    /* SEE FINAL REPORT FOR EXPLANATION */
    public class OrderService
    {
         /* Private variable that connect the Controller to the Repo-Layer */
        private OrderRepo _orderRepo;

        /* Constructor: */
        public OrderService()
        {
            _orderRepo = new OrderRepo();
        }

    }
}
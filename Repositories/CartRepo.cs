using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;
using System;

namespace BookCave.Repositories
{
    public class CartRepo
    {
        private DataContext _db;
        public CartRepo()
        {
            _db = new DataContext();
        }
        public List<OrderListViewModel> GetUserOrder()
        {
            var orders = (from c in _db.Carts
                          join o in _db.Orders on c.UserId equals o.UserId
                          select new OrderListViewModel
                          {
                            UserId = o.UserId,
                            Address = o.Address,
                            City = o.City,
                            Country = o.Country,
                            Quantity = o.Quantity,
                            Price = o.Price

                          }).ToList();
            return orders;
        }
    }
}
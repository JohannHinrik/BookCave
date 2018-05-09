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
                              OrderId = c.OrderId
                          }).ToList();
                          
                          return orders;
        }
    }
}
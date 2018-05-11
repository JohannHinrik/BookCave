using BookCave.Data;

namespace BookCave.Repositories
{
    public class OrderRepo
    {
        private DataContext _db;
        
        public OrderRepo()
        {
            _db = new DataContext();
        }
         
        public void AddToOrderHistory(string userId)
        {/* 
            var order = (from c in _db.Carts 
                         where c.userId == userId
                         select c).FirstOrDefault();

            var newOrder = new Order()
            {    
                UserId = userId, 
                BookId =
                Address
                City 
                Country 
                Quantity 
                Price

            }*/

        }

        
    }
}
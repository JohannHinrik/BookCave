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
        { 
            

        }

        
    }
}
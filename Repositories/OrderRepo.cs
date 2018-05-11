using BookCave.Data;

namespace BookCave.Repositories
{
    public class OrderRepo
    {
        /* Private variable that connect the Controller to the Repo-Layer */
        private DataContext _db;

        /* Constructor: */
        public OrderRepo()
        {
            _db = new DataContext();
        }
         
        public void AddToOrderHistory(string userId)
        { 
            

        }

        
    }
}
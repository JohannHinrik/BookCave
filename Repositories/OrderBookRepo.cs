using BookCave.Data;

namespace BookCave.Repositories
{
    public class OrderBookRepo
    {
        /* Private variable that connect the Controller to the Repo-Layer */
        private DataContext _db;
        
        /* Constructor: */
        public OrderBookRepo()
        {
            _db = new DataContext();
        }
    }
}
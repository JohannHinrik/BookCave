using BookCave.Data;

namespace BookCave.Repositories
{
    // SEE FINAL REPORT FOR EXPLANATIONS 
    public class OrderBookRepo
    {
        // Private variable that connect the Controller to the database 
        private DataContext _db;

        // Constructor: 
        public OrderBookRepo()
        {
            _db = new DataContext();
        }
    }
}
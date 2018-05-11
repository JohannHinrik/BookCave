using BookCave.Data;

namespace BookCave.Repositories
{
    // SEE FINAL REPORT FOR EXPLANATIONS 
    public class OrderRepo
    {
        // Private variable that connect the Controller to the database 
        private DataContext _db;

        // Constructor: 
        public OrderRepo()
        {
            _db = new DataContext();
        }
        
    }
}
using BookCave.Data;

namespace BookCave.Repositories
{
    public class CreditCardRepo
    {
        private DataContext _db;
        
        public CreditCardRepo()
        {
            _db = new DataContext();
        }
        
        public void ShowCard()
        {
            
        }
    }
}
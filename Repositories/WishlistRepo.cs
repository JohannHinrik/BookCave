using BookCave.Data;

namespace BookCave.Repositories
{
    public class WishlistRepo
    {
        private DataContext _db;
        public WishlistRepo()
        {
            _db = new DataContext();
        }

        
    }
}
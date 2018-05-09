using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;

namespace BookCave.Repositories
{
    public class ReviewRepo
    {
        private DataContext _db;
        
        public ReviewRepo()
        {
            _db = new DataContext();
        }

        public List<ReviewListViewModel> GetAllReviews(int id)
        {
            // The function should return all the reviews where the parameter ID == their bookID :
            var reviewList = (from r in _db.Reviews
                              join b in _db.Books on r.BookId equals b.Id
                              where b.Id == id
                              select new ReviewListViewModel
                              {
                                    BookId = r.Id,
                                    AccountId = r.AccountId,
                                    Comment = r.Comment,
                                    Id = r.Id,
                                    Rating = r.Rating
                              }).ToList();
            return reviewList;
        }
    }
    
}

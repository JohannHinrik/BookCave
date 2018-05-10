using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;
using BookCave.Data.EntityModels;

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
                                    //AccountId = r.AccountId,
                                    Comment = r.Comment,
                                    Id = r.Id,
                                    Rating = r.Rating
                              }).ToList();
            return reviewList;
        }

        public int FindReviewID()
        {
            var newId = 2;
            return newId; 
        }

        public int FindAccountId()
        {
            var newId = 5;
            return newId;
        }

        public int FindBookId()
        {
            var newId = 1;
            return newId;
        }

        public void AddReviewToDB(ReviewListViewModel NewReview)
        {
                var newReview = new Review()
                {
                    BookId = NewReview.BookId,
                    AccountId = NewReview.AccountId,
                    Comment = NewReview.Comment,
                    Rating = NewReview.Rating
                };
            _db.Add(newReview);
            _db.SaveChanges();
            return;
        }
    }
    
}

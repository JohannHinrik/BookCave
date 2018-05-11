using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class ReviewService
    {
        private ReviewRepo _reviewRepo;
        public ReviewService()
        {
            _reviewRepo = new ReviewRepo();
        }
        public List<ReviewListViewModel> GetAllReviews(int Id)
        {
            var ReviewList = _reviewRepo.GetAllReviews(Id);
            return ReviewList;
        }
        /* 
               public string FindAccountId()
                {
                    var newId = _reviewRepo.FindAccountIdAsync();
                    return newId;
                }
        */

        public int FindBookId()
        {
            var bookId = _reviewRepo.FindBookId();
            return bookId;
        }

        public void AddReviewToDB(ReviewListViewModel NewReview)
        {
            _reviewRepo.AddReviewToDB(NewReview);
            return;
        }
    }
}




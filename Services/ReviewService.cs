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

        public int FindReviewID()
        {
            var newId = _reviewRepo.FindReviewID();
            return newId;
        }

       /* public int FindAccountId()
        {
            var newId = _reviewRepo.FindAccountId()
            return newId;
        }*/

        public int FindBookId()
        {
            var newId = _reviewRepo.FindBookId();
            return newId;
        }

        public void AddReviewToDB(ReviewListViewModel NewReview)
        {
            _reviewRepo.AddReviewToDB(NewReview);
            return;
        }
    }
}




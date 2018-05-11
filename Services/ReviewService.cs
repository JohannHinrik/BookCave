using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class ReviewService
    {
        // Private variable that connect the Controller to the Repo-Layer 
        private ReviewRepo _reviewRepo;

        // Constructor: 
        public ReviewService()
        {
            _reviewRepo = new ReviewRepo();
        }

        // Function that returns a list of all recview for a chosen book from the repository layer 
        public List<ReviewListViewModel> GetAllReviews(int Id)
        {
            var ReviewList = _reviewRepo.GetAllReviews(Id);
            return ReviewList;
        }

        // Void function that adds a review to the database from the repository layer 
        public void AddReviewToDB(ReviewListViewModel NewReview)
        {
            _reviewRepo.AddReviewToDB(NewReview);
            return;
        }
    }
}




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
    }
}




/// _reviewService.GetAllReviews(Id));
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
    }
}
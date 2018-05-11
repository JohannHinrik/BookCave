using System;
using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;
using BookCave.Data.EntityModels;

namespace BookCave.Repositories
{
    public class ReviewRepo
    {
        // Private variable that connect the Controller to the database 
        private DataContext _db;

        // Constructor: 
        public ReviewRepo()
        {
            _db = new DataContext();
        }


        // Function that returns all the reviews from the database 
        public List<ReviewListViewModel> GetAllReviews(int id)
        {
            // The function should return all the reviews where the parameter ID == their bookID :
            var reviewList = (from r in _db.Reviews
                              join b in _db.Books on r.BookId equals b.Id
                              where b.Id == id
                              select new ReviewListViewModel
                              {
                                  BookId = r.Id,
                                  Comment = r.Comment,
                                  Id = r.Id,
                                  Rating = r.Rating
                              }).ToList();
            return reviewList;
        }

        /* Function that adds a user review to the database */
        public void AddReviewToDB(ReviewListViewModel NewReview)
        {
            // An instance of Review (entity model) is made from the ReviewListModel in the parameters
            var newReview = new Review()
            {
                BookId = NewReview.BookId,
                AccountId = NewReview.AccountId,
                Comment = NewReview.Comment,
                Rating = NewReview.Rating
            };
            
            // The instance is added to the database
            _db.Add(newReview);
            _db.SaveChanges();
            return;
        }
    }

}

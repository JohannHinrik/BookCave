using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;

namespace BookCave.Repositories
{
    public class AuthorRepo
    {
        // Private variable that connect the Controller to the database 
        private DataContext _db;

        // Constructor: 
        public AuthorRepo()
        {
            _db = new DataContext();
        }

        // function that returns all authors from database: 
        public List<AuthorListViewModel> GetAllAuthors()
        {
            var authors = (from a in _db.Authors
                           select new AuthorListViewModel
                           {
                               Id = a.Id,
                               Name = a.Name
                           }).ToList();
            return authors;
        }

        // function that returns the top rated authors from database 
        public List<AuthorListViewModel> GetTopRatedAuthors()
        {
            // Gets a list of all the user ratings the author has resieved for all his books
            var topRatedBooks = (from b in _db.Books
                                 join r in _db.Reviews on b.Id equals r.BookId
                                 select new AuthorListViewModel
                                 {
                                     Id = b.AuthorId,
                                     Rating = r.Rating
                                 });


            // Top rated authors are calculated from the table above,
            // by getting the average of the authors user ratings
            var topRatedAuthors = (from a in _db.Authors
                                   join b in topRatedBooks on a.Id equals b.Id
                                   orderby b.Rating descending
                                   group b by new { b.Id, a.Name } into BooksRated
                                   select new AuthorListViewModel
                                   {
                                       Name = BooksRated.Key.Name,
                                       Rating = BooksRated.Average(a => a.Rating)
                                   });

            // Making sure the list is ordered correctly
            var topTen = topRatedAuthors.OrderByDescending(average => average.Rating);

            return topTen.Take(10).ToList();
        }
    }
}
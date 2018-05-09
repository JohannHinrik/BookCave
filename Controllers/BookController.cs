using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using BookCave.Data.EntityModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BookCave.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;
        private AuthorService _authorService;
        private ReviewService _reviewService;

        public BookController()
        {
            _bookService = new BookService();
            _authorService = new AuthorService();
            _reviewService = new ReviewService();
        }
        /*public IActionResult Index()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }*/

        public IActionResult Index(int genre, int order, string search)
        {
            if (search == null) 
            {
                var books = _bookService.GetAllBooks();
                return View(books);
            }
            else 
            {
                var filteredBooks = _bookService.GetSearchedBooks(genre, order, search);
                return View(filteredBooks);           
            }
        }

///TODO: TAKA ÞENNANN ÚT???:
        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult TopRated()
        {
           var topRatedBooks = _bookService.GetTopRatedBooks();
           var topRatedAuthors = _authorService.GetTopRatedAuthors(); 

            var topRated = new Tuple<List<BookListViewModel>, List<AuthorListViewModel>>(_bookService.GetTopRatedBooks(),_authorService.GetTopRatedAuthors());
             return View(topRated);
        }
        public IActionResult Details(int Id)          
        {
            //bookDetails keeps the book details and all the reviews from the users (from DB):
            var bookDetails = new Tuple<BookListViewModel, List<ReviewListViewModel>>(_bookService.GetBookDetails(Id),_reviewService.GetAllReviews(Id));

            if(bookDetails == null)
            {
                return View("Error");
            }
            return View(bookDetails);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Details(ReviewListViewModel review)     
        {
            //If the comment was not valid:
            if(ModelState.IsValid)
            {
                var newReview = new Review()
                {
                    BookId = _reviewService.FindBookId(),
                    AccountId = _reviewService.FindAccountId(),
                    Comment = review.Comment,
                    Id = _reviewService.FindReviewID(),
                    Rating =  review.Rating
                };
                _reviewService.AddReviewToDB();
                return View();
            }
            return View();
        }

/*        [HttpPost]
        public IActionResult Create(MovieInputModel movie)
        {
            A new instance of the entity Model Movie is made from the movie in the parameter:
            if(ModelState.IsValid)
            {
                var newMovie = new Movie()
                {
                    Id = FakeDatabase.Movies.Count + 1, 
                    Title = movie.Title, 
                    ReleaseYear = movie.ReleaseYear, 
                    Runtime = movie.Runtime, 
                    Genre = movie.Genre, 
                    Image = "", 
                    Rating = 0
                };
                //Back to Movie menu:
                FakeDatabase.Movies.Add(newMovie);
                return RedirectToAction("Index");
            }
            return View();
        } */








        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

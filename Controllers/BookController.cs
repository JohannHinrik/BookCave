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
            if (search == null && !(order == 0 && genre == 0)) 
            {
                var filteredBooks = _bookService.GetSearchedBooks(genre, order);
                return View(filteredBooks);
            }
            else if (search == null) 
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

            var topRated = new Tuple<List<BookListViewModel>,List<AuthorListViewModel>>(_bookService.GetTopRatedBooks(),_authorService.GetTopRatedAuthors());
             return View(topRated);
        }
        public IActionResult Details(int Id)          
        {
            //bookDetails keeps the book details and all the reviews from the users (from DB):
            var bookDetails = new Tuple<BookListViewModel, ReviewListViewModel ,List<ReviewListViewModel>>(_bookService.GetBookDetails(Id),null,_reviewService.GetAllReviews(Id));

            if(bookDetails == null)
            {
                return View("Error");
            }
            return View(bookDetails);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Details(ReviewListViewModel review, int id)     
        {            
            //If the comment was not valid:
            if(ModelState.IsValid)
            {
                var newReview = new ReviewListViewModel()
                {
                    BookId = id,
                    AccountId = _reviewService.FindAccountId(), //MUNA að breyta þessu
                    Comment = review.Comment,
                    Rating =  review.Rating
                };
                _reviewService.AddReviewToDB(newReview);
                var bookDetails1 = new Tuple<BookListViewModel, ReviewListViewModel ,List<ReviewListViewModel>>(_bookService.GetBookDetails(id),null,_reviewService.GetAllReviews(id));
                return View(bookDetails1);
            }
            var bookDetails2 = new Tuple<BookListViewModel, ReviewListViewModel ,List<ReviewListViewModel>>(_bookService.GetBookDetails(id),null,_reviewService.GetAllReviews(id));
            return View(bookDetails2);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

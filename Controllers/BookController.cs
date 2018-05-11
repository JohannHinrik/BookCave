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
using Microsoft.AspNetCore.Identity;

namespace BookCave.Controllers
{
    public class BookController : Controller
    {
        /* Private variables that connect the Controller to the Service-Layers */
        private BookService _bookService;
        private AuthorService _authorService;
        private ReviewService _reviewService;

        /* Constructor: */
        public BookController()
        {
            _bookService = new BookService();
            _authorService = new AuthorService();
            _reviewService = new ReviewService();
        }

         /* Function that returns the front page, takes in search-bar parameters: */       
        public IActionResult Index(int genre, int order, string search)
        {
            if (search == null && !(order == 0 && genre == 0)) 
            {
                // filters the books depending on the search-bar
                var filteredBooks = _bookService.GetSearchedBooks(genre, order);
                return View(filteredBooks);
            }
            else if (search == null) 
            {
                // If nothing was searched the View returns all books 
                var books = _bookService.GetAllBooks();
                return View(books);
            } 
            else 
            {
                // filters the books depending on the search-bar
                var filteredBooks = _bookService.GetSearchedBooks(genre, order, search);
                return View(filteredBooks);           
            }
        }

         /* Function that returns  the top rated books and the top rated authors to the TopRated view: */       
        public IActionResult TopRated()
        {
            // Tuple gets the top rated authors and top rated books from service layer
            var topRated = new Tuple<List<BookListViewModel>,List<AuthorListViewModel>>(_bookService.GetTopRatedBooks(),_authorService.GetTopRatedAuthors());
             return View(topRated);
        }

         /* Function that returns a chosen books (id) details: */       
        public IActionResult Details(int Id)          
        {
            // Tuple keeps the book details, one empty reviewModel (null) and a list of all the reviews from the users (from DB):
            var bookDetails = new Tuple<BookListViewModel, ReviewListViewModel ,List<ReviewListViewModel>>(_bookService.GetBookDetails(Id),null,_reviewService.GetAllReviews(Id));

            if(bookDetails == null)
            {
                return View("Error");
            }
            return View(bookDetails);
        }

         /* Function that returns an error page: */       
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

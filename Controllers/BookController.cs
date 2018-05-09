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

            var topRated = new Tuple<List<BookListViewModel>, List<AuthorListViewModel>>(_bookService.GetTopRatedBooks(),_authorService.GetTopRatedAuthors());
             return View(topRated);
        }
        public IActionResult Details(int Id)          
        {
            var bookDetails = new Tuple<BookListViewModel, List<ReviewListViewModel>>(_bookService.GetBookDetails(Id),_reviewService.GetAllReviews(Id));

            if(bookDetails == null)
            {
                return View("Error");
            }
            return View(bookDetails);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

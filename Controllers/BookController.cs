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
<<<<<<< HEAD
using Microsoft.AspNetCore.Authorization;
=======
using BookCave.Repositories;
>>>>>>> df1715eac28b2285c7625949e520ec223a000b79

namespace BookCave.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private BookService _bookService;
        private AuthorService _authorService;

        public BookController()
        {
            _bookService = new BookService();
            _authorService = new AuthorService();
        }
       /* public IActionResult Index()
        {
           
        }*/
        public IActionResult Index(string search)
        {
            if (search == null) 
            {
                var books = _bookService.GetAllBooks();
                return View(books);
            }
            else 
            {
                var filteredBooks = _bookService.GetSearchedBooks(search);
                return View(filteredBooks.ToList());           
            }
        }

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
            var bookDetails = _bookService.GetBookDetails(Id);
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

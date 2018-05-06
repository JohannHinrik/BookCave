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

namespace BookCave.Controllers
{
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
          /* var topRatedAuthors = _authorService.GetTopRatedAuthors(); */

/*            var topRated = new Tuple<List<Book>, List<Author>>(_bookService.GetTopRatedBooks(),_authorService.GetTopRatedAuthors());
 */            return View(topRatedBooks);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

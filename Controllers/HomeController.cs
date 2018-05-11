using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using BookCave.Data;
using BookCave.Models.ViewModels;
using BookCave.Data.EntityModels;
using BookCave.Repositories;

namespace BookCave.Controllers
{
    public class HomeController : Controller
    {
        /* Private variables that connect the Controller to the Service-Layers */
        private AuthorService _authorService;
        private BookService _bookService;

        /* Constructor: */
        public HomeController()
        {
            _authorService = new AuthorService();
            _bookService = new BookService();
        }

        /* Function that returns the front page: */
        public IActionResult Index()
        {
            // A short list of top rated books is returned to the view
            var book = _bookService.GetTopRatedBooks();
            return View(book);
        }
        
        /* Function that returns the About view: */
        public IActionResult About()
        {
            return View();
        } 

         /* Function that returns an Error page */
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
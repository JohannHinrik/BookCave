using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
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
        private AuthorService _authorService;
        private BookService _bookService;
        public HomeController()
        {
            _authorService = new AuthorService();
            _bookService = new BookService();
        }
        public IActionResult Index()
        {
            var book = _bookService.GetTopRatedBooks();
            return View(book);
        }
        public IActionResult About()
        {
            return View();
        } 
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                //Logging the error if necessary
                string path = exceptionFeature.Path;
                Exception ex = exceptionFeature.Error;
            }
            //Returning the default error view
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;

namespace BookCave.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;
        public BookController()

        // Constructor :
         public BookController()
        {
            _bookService = new BookService();
        }

        // Index-view:
        public IActionResult Index()
        {
            var Books = _bookService.GetAllBooks();
            
            return View(Books);
        }
    }
}

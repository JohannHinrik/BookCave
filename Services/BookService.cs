using System.Collections.Generic;
using BookCave.Model.ViewModels;
//using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class BookService
    {
        private BookRepo _bookRepo;

        //Constructor:
        public BookService()
        {
            _bookRepo = new BookRepo();
        }

        //GetAllBooks function: 
        public List<BookListViewModel> GetAllBooks()
        {
            var books = _bookRepo.GetAllBooks();
            return books;
        }
    }
}
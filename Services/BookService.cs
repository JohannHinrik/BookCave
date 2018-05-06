using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class BookService
    {
        private BookRepo _bookRepo;
        public BookService()
        {
            _bookRepo = new BookRepo();
        }
        public List<BookListViewModel> GetAllBooks()
        {
            var books = _bookRepo.GetAllBooks();
            return books;
        }
        public List<BookListViewModel> GetTopRatedBooks()
        {
            var topRatedBooks = _bookRepo.GetTopRatedBooks();
            return topRatedBooks;
        }
        public List<BookListViewModel> GetSearchedBooks(string search)
        {
            var searchedBooks = _bookRepo.GetSearchedBooks(search);
            return searchedBooks;
        }
        public BookListViewModel GetBookDetails(int Id)
        {
            var book = _bookRepo.GetBookDetails(Id);
            return book;
        }
    }
}
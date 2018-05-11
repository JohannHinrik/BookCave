using System;
using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class BookService
    {

         // Private variable that connect the Controller to the Repo-Layer 
        private BookRepo _bookRepo;


        // Constructor: 
        public BookService()
        {
            _bookRepo = new BookRepo();
        }

        // Function that returns a list of all books in database from the repository layer 
        public List<BookListViewModel> GetAllBooks()
        {
            var books = _bookRepo.GetAllBooks();
            return books;
        }

        // Function that returns a list of top rated books from the repository layer 
        public List<BookListViewModel> GetTopRatedBooks()
        {
            var topRatedBooks = _bookRepo.GetTopRatedBooks();
            return topRatedBooks;
        }


        // Function that returns a list of searched books from the repository layer 
        public List<BookListViewModel> GetSearchedBooks(int genre, int order, string search)
        {
            var searchedBooks = _bookRepo.GetSearchedBooks(genre, order, search);
            return searchedBooks;
        }

        // Function that returns a chosen book (id) from the repository layer 
        public BookListViewModel GetBookDetails(int Id)
        {
            var book = _bookRepo.GetBookDetails(Id);
            return book;
        }

        // Function that returns a list of searched books from the repository layer 
        public List<BookListViewModel> GetSearchedBooks(int genre, int order)
        {
            var searchedBooks = _bookRepo.GetSearchedBooks(genre, order);
            return searchedBooks;
        }
    }
}
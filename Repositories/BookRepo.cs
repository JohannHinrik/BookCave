using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;
using System;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        // Private variable that connect the Controller to the database 
        private DataContext _db;

        // Constructor: 
        public BookRepo()
        {
            _db = new DataContext();
        }

        // function that returns a list of all the books in the database 
        public List<BookListViewModel> GetAllBooks()
        {
            var getRating = (from b in _db.Books
                             join r in _db.Reviews on b.Id equals r.BookId
                             group r by new { r.BookId, b.Id } into BooksRated
                             select new BookListViewModel
                             {
                                 BookId = BooksRated.Key.Id,
                                 Rating = BooksRated.Average(a => a.Rating)
                             });

            var books = (from b in _db.Books
                         join au in _db.Authors on b.AuthorId equals au.Id
                         join r in getRating on b.Id equals r.BookId
                         select new BookListViewModel
                         {
                             BookId = b.Id,
                             Title = b.Title,
                             Genre = b.Genre,
                             About = b.About,
                             Rating = r.Rating,
                             Author = au.Name,
                             Price = b.Price
                         }).ToList();
            return books;
        }

        // Function that returns the top user rated books from database 
        public List<BookListViewModel> GetTopRatedBooks()
        {
            // Average of the user ratings from each book is calculated
            var topRatedBooks = (from b in _db.Books
                                 join r in _db.Reviews on b.Id equals r.BookId
                                 group r by new { r.BookId, b.Id } into BooksRated
                                 select new BookListViewModel
                                 {
                                     BookId = BooksRated.Key.Id,
                                     Rating = BooksRated.Average(a => a.Rating)
                                 });

            // Top rated books are ordered in descending order 
            var topTen = topRatedBooks.OrderByDescending(average => average.Rating);
            
            // The rating found and put in the new instance fo the BookListViewModel 
            var topReturn = (from b in _db.Books
                             join au in _db.Authors on b.AuthorId equals au.Id
                             join r in topTen on b.Id equals r.BookId
                             orderby r.Rating descending
                             select new BookListViewModel
                             {
                                 BookId = b.Id,
                                 Title = b.Title,
                                 Genre = b.Genre,
                                 About = b.About,
                                 Rating = r.Rating,
                                 Author = au.Name,
                                 Price = b.Price
                             }).Take(10);
            return topReturn.ToList();
        }
        // function that uses the search-field to return a list of searched books 
        public List<BookListViewModel> GetSearchedBooks(int genre, int order)
        {
            var getRating = (from b in _db.Books
                             join r in _db.Reviews on b.Id equals r.BookId
                             group r by new { r.BookId, b.Id } into BooksRated
                             select new BookListViewModel
                             {
                                 BookId = BooksRated.Key.Id,
                                 Rating = BooksRated.Average(a => a.Rating)
                             });

            var filteredBooks = (from b in _db.Books
                                 join au in _db.Authors on b.AuthorId equals au.Id
                                 join r in getRating on b.Id equals r.BookId
                                 select new BookListViewModel
                                 {
                                     BookId = b.Id,
                                     Title = b.Title,
                                     Genre = b.Genre,
                                     About = b.About,
                                     Rating = r.Rating,
                                     Author = au.Name,
                                     Price = b.Price
                                 });
            // Genres 
            if (genre == 1)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Adventure"));
            }
            else if (genre == 2)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Autobiography"));
            }
            else if (genre == 3)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Fiction"));
            }
            else if (genre == 4)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Studies"));
            }
            else if (genre == 5)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Romance"));
            }
            else if (genre == 6)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Cookbooks"));
            }
            else if (genre == 7)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("ScienceFiction"));
            }
            else if (genre == 8)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Novel"));
            }

            // Orders 
            if (order == 1)
            {
                filteredBooks = filteredBooks.OrderBy(book => book.Price);
            }
            else if (order == 2)
            {
                filteredBooks = filteredBooks.OrderByDescending(book => book.Price);
            }
            else if (order == 3 || order == 0)
            {
                filteredBooks = filteredBooks.OrderBy(book => book.Title);
            }
            else if (order == 4)
            {
                filteredBooks = filteredBooks.OrderByDescending(book => book.Title);
            }

            // Return value 
            var output = filteredBooks.ToList();
            return output;
        }

        public List<BookListViewModel> GetSearchedBooks(int genre, int order, string search)
        {
            var getRating = (from b in _db.Books
                             join r in _db.Reviews on b.Id equals r.BookId
                             group r by new { r.BookId, b.Id } into BooksRated
                             select new BookListViewModel
                             {
                                 BookId = BooksRated.Key.Id,
                                 Rating = BooksRated.Average(a => a.Rating)
                             });

            var filteredBooks = (from b in _db.Books
                                 join au in _db.Authors on b.AuthorId equals au.Id
                                 where b.Title.ToLower().Contains(search.ToLower())
                                       || au.Name.ToLower().Contains(search.ToLower())
                                 join r in getRating on b.Id equals r.BookId
                                 select new BookListViewModel
                                 {
                                     BookId = b.Id,
                                     Title = b.Title,
                                     Genre = b.Genre,
                                     About = b.About,
                                     Rating = r.Rating,
                                     Author = au.Name,
                                     Price = b.Price
                                 });
            // Genres 
            if (genre == 1)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Adventure"));
            }
            else if (genre == 2)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Autobiography"));
            }
            else if (genre == 3)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Fiction"));
            }
            else if (genre == 4)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Studies"));
            }
            else if (genre == 5)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Romance"));
            }
            else if (genre == 6)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Cookbooks"));
            }
            else if (genre == 7)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("ScienceFiction"));
            }
            else if (genre == 8)
            {
                filteredBooks = filteredBooks.Where(book => book.Genre.Equals("Novel"));
            }

            // Orders 
            if (order == 1)
            {
                filteredBooks = filteredBooks.OrderBy(book => book.Price);
            }
            else if (order == 2)
            {
                filteredBooks = filteredBooks.OrderByDescending(book => book.Price);
            }
            else if (order == 3 || order == 0)
            {
                filteredBooks = filteredBooks.OrderBy(book => book.Title);
            }
            else if (order == 4)
            {
                filteredBooks = filteredBooks.OrderByDescending(book => book.Title);
            }

            // Return value 
            var output = filteredBooks.ToList();
            return output;
        }
        // Function that returns a BookListViewModel with all the info from the book with the chosen ID 
        public BookListViewModel GetBookDetails(int Id)
        {
            // Get the rating of the book
            var getRating = (from b in _db.Books
                             join r in _db.Reviews on b.Id equals r.BookId
                             group r by new { r.BookId, b.Id } into BooksRated
                             select new BookListViewModel
                             {
                                 BookId = BooksRated.Key.Id,
                                 Rating = BooksRated.Average(a => a.Rating)
                             });

            // Get the info from the database about the book and stores it in a new instance of BookListViewModel
            var book = (from b in _db.Books
                        join au in _db.Authors on b.AuthorId equals au.Id
                        where b.Id == Id
                        join r in getRating on b.Id equals r.BookId
                        select new BookListViewModel
                        {
                            BookId = b.Id,
                            Title = b.Title,
                            Genre = b.Genre,
                            About = b.About,
                            Author = au.Name,
                            Rating = r.Rating,
                            Price = b.Price
                        }).FirstOrDefault();
            return book;
        }
    }
}
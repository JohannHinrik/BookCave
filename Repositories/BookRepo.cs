using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;
using System;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        /* Private variable that connect the Controller to the Repo-Layer */
        private DataContext _db;

        /* Constructor: */
        public BookRepo()
        {
            _db = new DataContext();
        }
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
        public List<BookListViewModel> GetTopRatedBooks()
        {
            var topRatedBooks = (from b in _db.Books
                                 join r in _db.Reviews on b.Id equals r.BookId
                                 group r by new { r.BookId, b.Id } into BooksRated
                                 select new BookListViewModel
                                 {
                                     BookId = BooksRated.Key.Id,
                                     Rating = BooksRated.Average(a => a.Rating)
                                 });

            var topTen = topRatedBooks.OrderByDescending(average => average.Rating);

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
            //GENReS
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

            //ORDERS
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

            //RETURN VALUE
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
            //GENReS
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

            //ORDERS
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

            //RETURN VALUE
            var output = filteredBooks.ToList();
            return output;
        }

        public BookListViewModel GetBookDetails(int Id)
        {
            var getRating = (from b in _db.Books
                             join r in _db.Reviews on b.Id equals r.BookId
                             group r by new { r.BookId, b.Id } into BooksRated
                             select new BookListViewModel
                             {
                                 BookId = BooksRated.Key.Id,
                                 Rating = BooksRated.Average(a => a.Rating)
                             });

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
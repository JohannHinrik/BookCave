using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        private DataContext _db;
        
        public BookRepo()
        {
            _db = new DataContext();
        }

        public List<BookListViewModel> GetAllBooks(int genre, int order)
        {
            string tempGenre = "?";
            if (genre == 1)
            {
                tempGenre = "Adventure";
            }
            else if (genre == 2)
            {
                tempGenre = "Autobiography";
            }
            else if (genre == 3)
            {
                tempGenre = "Fiction";
            }
            var books = (from b in _db.Books
                    join au in _db.Authors on b.AuthorId equals au.Id
                    where(b.Genre.Contains(tempGenre))
                 //   if (order == 1) {books.Orderby = b.Price}
                    select new BookListViewModel
                    {
                        BookId = b.Id,
                        Title = b.Title,
                        Genre = b.Genre,
                        //ReviewId = b.Id,
                        About = b.About,
                        Rating = b.Rating,
                        Author = au.Name,
                        Price = b.Price
                    }).ToList();
            return books;  

            }
/*
            //price low to high
            if (order == 1)
            {
                books = (from b in books
                        orderby b.Price ascending
                        select b).ToList();
            }
            //price high to low
            else if (order == 2)
            {
                books = (from b in books
                        orderby b.Price
                        select b).ToList();
            }
            //alphabet
            else if (order == 3)
            {
                books = (from b in books
                        orderby b.Title
                        select b).ToList();
            }
            //reversed alphabet
            else if (order == 4)
            {
                books = (from b in books
                        orderby b.Title descending
                        select b).ToList();
            } */
        }
        public List<BookListViewModel> GetTopRatedBooks()
        {
            var topRatedbooks = (from b in _db.Books
                        join au in _db.Authors on b.AuthorId equals au.Id
                        orderby b.Rating descending
                        select new BookListViewModel
                        {
                            BookId = b.Id,
                            Title = b.Title,
                            Genre = b.Genre,
                            //ReviewId = b.Id,
                            About = b.About,
                            Rating = b.Rating,
                            Author = au.Name,
                            Price = b.Price
                        }).Take(10).ToList();
            return topRatedbooks;
        }

        public List<BookListViewModel> GetSearchedBooks(int genre, int order, string search)
        {
            string tempGenre = "?";
            if (genre == 1)
            {
                tempGenre = "Adventure";
            }
            else if (genre == 2)
            {
                tempGenre = "Autobiogrophy";
            }
            else if (genre == 3)
            {
                tempGenre = "Fiction";
            }

            var filteredBooks = (from b in _db.Books
                        join au in _db.Authors on b.AuthorId equals au.Id
                        where ((b.Title.ToLower().Contains(search.ToLower())) 
                              || (au.Name.ToLower().Contains(search.ToLower())))
                              && b.Genre.Contains(tempGenre)
                              /*|| (b.Genre.ToLower() == search.ToLower())*/
                        select new BookListViewModel
                        {
                            BookId = b.Id,
                            Title = b.Title,
                            Genre = b.Genre,
                            About = b.About,
                            Rating = b.Rating,
                            Author = au.Name,
                            Price = b.Price
                        }).ToList();
            return filteredBooks;
        }

        public BookListViewModel GetBookDetails(int Id)
        {
            var book = (from b in _db.Books
                        join au in _db.Authors on b.AuthorId equals au.Id
                        where b.Id == Id
                        select new BookListViewModel
                        {
                            BookId = b.Id,
                            Title = b.Title,
                            Genre = b.Genre,
                            About = b.About,
                            Author = au.Name,
                            Rating = b.Rating,
                            Price = b.Price
                        }).First();
            return book;
        }
    }
}
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
            if (tempGenre == "Adventure" || tempGenre == "Autobiography" || tempGenre == "Fiction")
            {
                var books = (from b in _db.Books
                join au in _db.Authors on b.AuthorId equals au.Id
                where(b.Genre.Contains(tempGenre)) 
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
            else
            {    
                var books = (from b in _db.Books
                    join au in _db.Authors on b.AuthorId equals au.Id
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
            var filteredBooks = (from b in _db.Books
                        join au in _db.Authors on b.AuthorId equals au.Id
                        where ((b.Title.ToLower().Contains(search.ToLower())) 
                              || (au.Name.ToLower().Contains(search.ToLower()))
                              || (b.Genre.ToLower() == search.ToLower()))
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
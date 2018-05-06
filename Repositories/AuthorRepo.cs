using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;

namespace BookCave.Repositories
{
    public class AuthorRepo
    {
        private DataContext _db;
        public AuthorRepo()
        {
            _db = new DataContext();
        } 
        public List<AuthorListViewModel> GetAllAuthors()
        {
           var authors = (from a in _db.Authors
                         select new AuthorListViewModel
                         {
                             Id = a.Id,
                             Name = a.Name
                         }).ToList();
            return authors;
        }

        public List<AuthorListViewModel> GetTopRatedAuthors()
        {
            var topRatedAuthors = (from a in _db.Authors
                                    orderby a.Rating
                                    select new AuthorListViewModel
                                    {
                                       Name = a.Name,
                                       Rating = a.Rating,
                                       Id = a.Id 
                                    }).Take(10).ToList();
            return topRatedAuthors;
        }
    }
}
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
            var topRatedAuthors = ( from a in _db.Authors
                                    join b in _db.Books on a.Id equals b.AuthorId
                                    group b by new { b.AuthorId, a.Name} into AuthorBooks
                                    select new AuthorListViewModel
                                    {
                                       Name = AuthorBooks.Key.Name,
                                       Rating = AuthorBooks.Average(b => b.Rating)
                                    });

            //var grp2 = topRatedAuthors.GroupBy(a=>a.Name,(key,g)=>g.OrderByDescending(b=>b.Rating).First());

            //var grp = topRatedAuthors.GroupBy(a=>a.Name,(key,g)=>
            var topTen = topRatedAuthors.OrderByDescending(average => average.Rating).Take(10);
        return topTen.ToList();
        }
    }
}
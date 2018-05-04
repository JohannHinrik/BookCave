using System.Collections.Generic;
using BookCave.Models.ViewModels;

namespace BookCave.Repositories
{
    public class AuthorRepo
    {
        public List<AuthorListViewModel> GetAllAuthors()
        {
            var authors = new List<AuthorListViewModel>
            {
                new AuthorListViewModel { Name = "JK Rowling", Rating = 3.4 }
            };
            return authors;
        }
    }
}
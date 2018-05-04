using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class AuthorService
    {
        private AuthorRepo _authorRepo;
        public AuthorService()
        {
            _authorRepo = new AuthorRepo();
        }
        public List<AuthorListViewModel> GetAllAuthors()
        {
            var authors = _authorRepo.GetAllAuthors();
            return authors;
        }
    }
}
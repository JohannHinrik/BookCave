using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class AuthorService
    {

        // Private variable that connect the Controller to the Repo-Layer 
        private AuthorRepo _authorRepo;


        // Constructor: 
        public AuthorService()
        {
            _authorRepo = new AuthorRepo();
        }

        // function that returns a list of all authors from the repository layer 
        public List<AuthorListViewModel> GetAllAuthors()
        {
            var authors = _authorRepo.GetAllAuthors();
            return authors;
        }


        // function that returns a list of top authors from the repository layer 
        public List<AuthorListViewModel> GetTopRatedAuthors()
        {
            var topRatedAuthors = _authorRepo.GetTopRatedAuthors();
            return topRatedAuthors;
        }
    }
}
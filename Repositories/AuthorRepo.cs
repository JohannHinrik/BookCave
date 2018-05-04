using System.Collections.Generic
using BookCave.Models.ViewModels.AuthorViewModel

namespace BookCave.Repositories
{
    public class AuthorRepo
    {
        List<AuthorListViewModel> GetAllAuthors()
        {
            var authors = new List<AuthorViewModel>
            {
                new AuthorListViewModel
            }
        }
    }
}
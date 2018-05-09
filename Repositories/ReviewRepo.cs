using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;

namespace BookCave.Repositories
{
    public class ReviewRepo
    {
        private DataContext _db;
        
        public ReviewRepo()
        {
            _db = new DataContext();
        }
    }
    
}
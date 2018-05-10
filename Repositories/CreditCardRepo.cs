using System.Linq;
using BookCave.Data;
using BookCave.Models.ViewModels;

namespace BookCave.Repositories
{
    public class CreditCardRepo
    {
        private DataContext _db;
        
        public CreditCardRepo()
        {
            _db = new DataContext();
        }
        
        public void ShowCard()
        {
            var order = (from c in _db.CreditCards
                         join o in _db.Orders on c.Id equals o.Id
                         select new CreditCardViewModel
                         {
                             CreditCardId = o.Id,

                         }).ToList();
        }
    }
}
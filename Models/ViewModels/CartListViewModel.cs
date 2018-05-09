using System.Collections.Generic;
using BookCave.Data.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public string Book { get; set; }
        public List<Cart> CartItems { get; set; }
        public int CartTotal { get; set; }
    }
}
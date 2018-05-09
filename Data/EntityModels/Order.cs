using System.Collections.Generic;

namespace BookCave.Data.EntityModels
{
    public partial class Order
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int AccountId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}

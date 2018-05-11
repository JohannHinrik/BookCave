using System;
using Microsoft.AspNetCore.Http;

namespace BookCave.Data.EntityModels
{
    public class Cart
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public bool Payed { get; set; }
        public int Quantity { get; set; }
    }
}
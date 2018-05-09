using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BookCave.Data.EntityModels
{
    public class Cart
    {
        public string Id { get; set; }
        public int BookId { get; set; }
        public int AccountId{ get; set; }
    }
}
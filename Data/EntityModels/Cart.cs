using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BookCave.Data.EntityModels
{
    public class Cart
    {
        public int BookId { get; set; }
        public int UserId{ get; set; }
    }
}
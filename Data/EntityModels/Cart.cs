using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BookCave.Data.EntityModels
{
    public class Cart
    {
        [Key]
        public int BookId { get; set; }
        public int UserId{ get; set; }
    }
}
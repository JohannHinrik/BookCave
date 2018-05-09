using System.ComponentModel.DataAnnotations;

namespace BookCave.Data.EntityModels
{
    public class Cart
    {
        [Key]
        public string Id { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
        
    }
}
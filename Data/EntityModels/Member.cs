namespace BookCave.Data.EntityModels
{
    public class Member
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string FavoriteBook { get; set; }
        public int MemberId { get; set; }

        // public WishList Wislist  { get; set; }
        // public  Cart Cart { get; set; }
    }
  
}
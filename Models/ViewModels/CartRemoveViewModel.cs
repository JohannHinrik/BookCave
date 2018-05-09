namespace BookCave.ViewModels
{
    public class ShoppingCartRemoveViewModel
    {
        public int DeleteId { get; set; }
        public string Message { get; set; }
        public int CartTotal { get; set; }
        public int CartCount { get; set; }
        public int ItemCount { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookCave.Data;
using BookCave.Data.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Repositories
{
    public partial class CartRepo
    {
        private DataContext _db;
        
        public CartRepo()
        {
            _db = new DataContext();
        }
        string ShoppingCartId { get; set; }
        //public const string CartSessionKey = "CartId";
        /*public static CartRepo GetCart(HttpContext context)
        {
            var cart = new CartRepo();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }*/
        /*
        //A helper function to make call to the cart simple
        public static CartRepo GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }*/
        public void AddToCart(Book Book)
        {
            //Get the matching cart and Book instances
            var cartItem = _db.Carts.SingleOrDefault(
                c => c.Id == ShoppingCartId 
                && c.BookId == Book.Id);
 
            if (cartItem == null)
            {
                //Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    BookId = Book.Id,
                    Id = ShoppingCartId,
                    Count = 1
                };
                _db.Carts.Add(cartItem);
            }
            else
            {
                //If item exists add one to the quantity
                cartItem.Count++;
            }
            //Save the changes
            _db.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            //Getting the cart
            var cartItem = _db.Carts.Single(
                cart => cart.Id == ShoppingCartId);
 
            int itemCount = 0;
 
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _db.Carts.Remove(cartItem);
                }

                _db.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = _db.Carts.Where(
                cart => cart.Id == ShoppingCartId);
 
            foreach (var cartItem in cartItems)
            {
                _db.Carts.Remove(cartItem);
            }

            _db.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return _db.Carts.Where(
                cart => cart.Id == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            //Getting the count of each item in cart and sum them up
            int? count = (from cartItems in _db.Carts
                          where cartItems.Id == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            //If all entries are null return 0
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in _db.Carts
                              where cartItems.Id == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Book.Price).Sum();

            return total ?? decimal.Zero;
        }
        public int CreateOrder(Order order)
        {
            int orderTotal = 0;
 
            var cartItems = GetCartItems();
            //Iterate over the items in the cart, 
            //adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    BookId = item.BookId,
                    OrderId = order.Id,
                    UnitPrice = item.Book.Price,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Book.Price);
 
                _db.OrderDetails.Add(orderDetail);
 
            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;
 
            // Save the order
            _db.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.Id;
        }
        // We're using HttpContextBase to allow access to cookies.
        /*public string GetCartId(HttpContext context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }*/
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = _db.Carts.Where(
                c => c.Id == ShoppingCartId);
 
            foreach (Cart item in shoppingCart)
            {
                item.Id = userName;
            }
            _db.SaveChanges();
        }
    }
}
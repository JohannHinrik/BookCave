using System;
using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class WishlistService
    {
        /* Private variable that connect the Controller to the Repo-Layer */
        private WishlistRepo _wishlistRepo;

        /* Constructor: */
        public WishlistService()
        {
            _wishlistRepo = new WishlistRepo();
        }


        /* Function that returns a list of all books in the users wishlist from the repository layer */
        public List<BookListViewModel> GetBooks(string userId)
        {
            return _wishlistRepo.GetBooks(userId);
        }


        /* Void function that adds a book to the users wishlist in database through the repository layer */
        public void AddToWishlist(string userId, int bookId)
        {
            _wishlistRepo.AddToWishlist(userId, bookId);
        }
    }
}
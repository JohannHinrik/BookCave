using System;
using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class WishlistService
    {
        private WishlistRepo _wishlistRepo;
        public WishlistService()
        {
            _wishlistRepo = new WishlistRepo();
        }

        public List<BookListViewModel> GetBooks(string userId)
        {
            return _wishlistRepo.GetBooks(userId);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using BookCave.Data.EntityModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BookCave.Controllers
{
    public class CartController : Controller
    {
        private CartService _cartService;
          public CartController()
        {
            _cartService = new CartService();
        }

         public IActionResult Index()
        {
            var orders = _cartService.GetUserOrder();
            return View(orders);
        }
    }
        
}
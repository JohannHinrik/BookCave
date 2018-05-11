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
        // Private variables that connect the Controller to the Service-Layers 
        private CartService _cartService;

        // Constructor: 
        public CartController()
        {
            _cartService = new CartService();
        }
    }
}
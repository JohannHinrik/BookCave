using System;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class ShopController : Controller
    {

        public IActionResult Cart()
        {
            return View();
        }
    }
}
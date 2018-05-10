using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Models.ViewModels;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using BookCave.Services;

namespace BookCave.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private CartService _cartService;
        
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _cartService = new CartService();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            //Get user data
            var user = await _userManager.GetUserAsync(User);
            
            return View(new ProfileViewModel {
                FirstName = user.FirstName,
                LastName = user.LastName,
                FavoriteBook = user.FavoriteBook,
                City = user.City,
                Country = user.Country,
                Address = user.Address,
                Image = user.Image
                /* TODO: Later Add email and username */
            });
        }

 [Authorize]
        public async Task<IActionResult> EditAccount()
        {
            //Get user data
            var user = await _userManager.GetUserAsync(User);
                Debug.WriteLine(user.FirstName);

            return View(new ProfileViewModel {
                FirstName = user.FirstName,
                LastName = user.LastName,
                FavoriteBook = user.FavoriteBook,
                City = user.City,
                Country = user.Country,
                Address = user.Address,
                Image = user.Image
                /* TODO: Later Add email and username */
            });
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditAccount(ProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            //Update properties
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.FavoriteBook = model.FavoriteBook;
            user.City = model.City;
            user.Country = model.Country;
            user.Address = model.Address;
            user.Image = model.Image;

            await _userManager.UpdateAsync(user);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            //Returning the view if not valid.
            if (!ModelState.IsValid)
            {
                return View();
            }
            //If the register goes through 
            //make the username the same as the password.
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                ActiveAccount = true,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                City = model.City,
                Country = model.Country
            };
            //Creating the user with _userManager.
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //The user is now registered succesfully.
                //Add the concatenated first name and last name as fullName in claims.
                await _userManager.AddClaimAsync(user, new Claim("Name", $"{model.FirstName} {model.LastName}"));
                //Signing in the user
                await _signInManager.SignInAsync(user, false);

                //Taking the user back to the homepage
                return RedirectToAction("Index", "Home");
            }
            //If the upper didn't succeed, return the view
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //The views returned when the user signs in.
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            //If not return the view
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Views when the user signs out.
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddingToCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            _cartService.AddItem(userId, id);

            return RedirectToAction("Index","Book");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            _cartService.DeleteItem(userId, id);

            return RedirectToAction("Cart","Account");
        }

        /* [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateCart(int id, string amount)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            _cartService.UpdateCart(userId, id, amount);

            return RedirectToAction("Cart","Account");
        }
 */
        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var books = _cartService.GetBooks(userId);
            return View(books);
        }

    }
}
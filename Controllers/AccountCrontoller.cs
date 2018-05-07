using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Models.ViewModels;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BookCave.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //If the register goes through 
            //make the username the same as the password.
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
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
    }
}
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
using System.Collections.Generic;

namespace BookCave.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISignUpService _signUpService;

        private CartService _cartService;
        private ReviewService _reviewService;
        private BookService _bookService;
        private OrderService _orderService;
        private WishlistService _wishlistService;

        public IActionResult Index()
        {
            return View();
        }
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ISignUpService signUpService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _signUpService = signUpService;
            _cartService = new CartService();
            _reviewService = new ReviewService();
            _bookService = new BookService();
            _orderService = new OrderService();
            _wishlistService = new WishlistService();
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
                Image = user.Image,
                Email = user.Email,
                UserName = user.UserName
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
        public async Task<IActionResult> SignUp(SignUpViewModel model, string image)
        {
            //Returning the view if not valid.
            if (!ModelState.IsValid)
            {
                return View();
            }
            //Error Messages
            _signUpService.ProcessSignUp(model);
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
                Country = model.Country,
                Image = image
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateCart(int id, int amount)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            _cartService.UpdateCart(userId, id, amount);

            return RedirectToAction("Cart","Account");
        }

       [Authorize]
        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var books = _cartService.GetBooks(userId);
            return View(books);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details(ReviewListViewModel review, int id)     
        {            
            //If the comment was not valid:
            if(ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;

                var newReview = new ReviewListViewModel()
                {
                    BookId = id,
                    AccountId = user.Id,
                    Comment = review.Comment,
                    Rating =  review.Rating
                }; 
                _reviewService.AddReviewToDB(newReview);
                return RedirectToAction("Details", "Book", new { id = id});
            }
            return RedirectToAction("Details", "Book", new { id = id});
        }


        [Authorize]
        public async Task<IActionResult> FirstPaymentStep()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var checkout = new CheckoutViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName, 
                City = user.City,
                Country = user.Country, 
                Address = user.Address,
                Email = user.Email,
            };

            return View(checkout);
        }

        [Authorize]
        public async Task<IActionResult> OverviewStep(CheckoutViewModel checkout)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            // Tuple that keeps the chekout info and all books in the users cart: 
            var checkoutTuple = new Tuple<CheckoutViewModel, List<BookListViewModel>>(checkout, _cartService.GetBooks(userId));

            return View(checkoutTuple);
        }

        [Authorize]
        public async Task<IActionResult> ConfirmPay()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            //Get the user, so the FirstName variable can be called in the view:
            var checkout = new CheckoutViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName, 
                City = user.City,
                Country = user.Country, 
                Address = user.Address,
                Email = user.Email,
            };
                
            // 2. Make bool variable "payed" true for purchased books:
                _cartService.UpdateCartPay(userId);

            return View(checkout);
        }
        
        [Authorize]
        //The Wishlist view
        public async Task<IActionResult> Wishlist()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var books = _wishlistService.GetBooks(userId);
            return View(books);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            _wishlistService.AddToWishlist(userId, id);

            return RedirectToAction("Index","Book");
        }

       public async Task<IActionResult> OrderHistory()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id; 
            var userName = user.FirstName;

            var orderList = _cartService.getOrderList(userId, userName);

            return View(orderList); 

        }

    }
}
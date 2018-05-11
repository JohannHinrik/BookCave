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
        /* Private variables that enable authentication */
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISignUpService _signUpService;

        /* Private variables that connect the Controller to the Service-Layers */
        private CartService _cartService;
        private ReviewService _reviewService;
        private BookService _bookService;
        private OrderService _orderService;
        private WishlistService _wishlistService;

        /* Constructor: */
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

        /* Function that returns the front page: */
        public IActionResult Index()
        {
            return View();
        }

        /* Function that returns the Sign Up View: */
        public IActionResult SignUp()
        {
            return View();
        }

        /* Authorized Function that returns the users data to the MyProfile view: */
        [Authorize] 
        public async Task<IActionResult> MyProfile()
        {
            // User data is fetched: 
            var user = await _userManager.GetUserAsync(User);
            // The data is kept in an instance of a ProfileViewModel: 
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
            });
        }

        /* Authorized function that returns the view before the user submits his new account info: */
        [Authorize]
        public async Task<IActionResult> EditAccount()
        {
            // User data is fetched: 
            var user = await _userManager.GetUserAsync(User);
                Debug.WriteLine(user.FirstName);

            // Users old account info returned in the View: 
            return View(new ProfileViewModel {
                FirstName = user.FirstName,
                LastName = user.LastName,
                FavoriteBook = user.FavoriteBook,
                City = user.City,
                Country = user.Country,
                Address = user.Address,
                Image = user.Image
            });
        }


        /* Auhorized function that takes the users new account info as a ProfileViewModel parameter and updates the DB:  */
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditAccount(ProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            // Update user properties
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.FavoriteBook = model.FavoriteBook;
            user.City = model.City;
            user.Country = model.Country;
            user.Address = model.Address;
            user.Image = model.Image;

            // Updates the database
            await _userManager.UpdateAsync(user);
            return View(model);
        }

        /* Function that registers a new user to the database, takes the new user info and image as parameters: */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model, string image)
        {
            _signUpService.ProcessSignUp(model);

            // Returning the SignUp view if the SignUpViewModel is not valid.
            if (!ModelState.IsValid)
            {
                return View();
            }
            // If the register goes through, a new instance application user is made
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
            // Creating the user with _userManager.
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // The user is now registered succesfully.
                // Add the concatenated first name and last name as fullName in claims.
                await _userManager.AddClaimAsync(user, new Claim("Name", $"{model.FirstName} {model.LastName}"));
                // Signing in the user
                await _signInManager.SignInAsync(user, false);

                // Returning the user back to the homepage - now signed in
                return RedirectToAction("Index", "Home");
            }
            // If the upper didn't succeed, return the view
            return View();
        }
        
        /* Function that returns the Login View before the user has filled in his info: */
        public IActionResult Login()
        {
            return View();
        }
        
        /* Function that sends the user to front page, takes submitted users info as parameter: */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // If the model with the submitted info was valid
            if (!ModelState.IsValid)
            {
                return View();
            }
            // Check if the submitted info is registered in database
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            // If not succeeded return the LogIn View
            return View();
        }

        /* Function that logs out the currently logged in user: */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        /* Function that returns the AccessDenied view if the user did not exist in database: */
        public IActionResult AccessDenied()
        {
            return View();
        }


        /* Authorized function that adds a book to the users cart in database: */
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddingToCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            // Sends the request to the service layer - that sends it to the repository layer
            _cartService.AddItem(userId, id);

            return RedirectToAction("Index","Book");
        }

        /* Authorized function that delets an item from the users cart in the database: */
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            _cartService.DeleteItem(userId, id);

            return RedirectToAction("Cart","Account");
        }

        /* Authorized function that updates the cart in the database: */
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateCart(int id, int amount)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            _cartService.UpdateCart(userId, id, amount);

            return RedirectToAction("Cart","Account");
        }

        /* Authorized function that returns the list of books connected to the users cart to the Cart view: */
       [Authorize]
        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var books = _cartService.GetBooks(userId);
            return View(books);
        }

        /* Authorized function that resieves comment+rating (ReviewListViewModel) and the book id and adds the review to the database: */
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details(ReviewListViewModel review, int id)     
        {            
            // If the review is valid
            if(ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;
                // A new instance of ReviewListViewModel that holds the info from the parameter is made
                var newReview = new ReviewListViewModel()
                {
                    BookId = id,
                    AccountId = user.Id,
                    Comment = review.Comment,
                    Rating =  review.Rating
                }; 
                // The instance is added to the database through the sevice layer
                _reviewService.AddReviewToDB(newReview);
                return RedirectToAction("Details", "Book", new { id = id});
            }
            return RedirectToAction("Details", "Book", new { id = id});
        }


        /* Authorized function that returns the users info through an instance of CheckoutViewModel to the FirstPaymentStep view: */
        [Authorize]
        public async Task<IActionResult> FirstPaymentStep()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            // The users info is kept in an instance of CheckoutViewModel
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

        /* Authorized function that takes in the submitted user info from the first payment step and returns it in OviewView view: */
        [Authorize]
        public async Task<IActionResult> OverviewStep(CheckoutViewModel checkout)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            // Tuple that keeps the checkout info and all books in the users cart: 
            var checkoutTuple = new Tuple<CheckoutViewModel, List<BookListViewModel>>(checkout, _cartService.GetBooks(userId));

            return View(checkoutTuple);
        }


        /* Authorized function that gives thanks to the user for buying the books: */
        [Authorize]
        public async Task<IActionResult> ConfirmPay()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            // Get the user, so the FirstName variable can be called in the view:
            var checkout = new CheckoutViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName, 
                City = user.City,
                Country = user.Country, 
                Address = user.Address,
                Email = user.Email,
            };
                
            // 2. Make bool variable "payed" in the cart database turn true:
                _cartService.UpdateCartPay(userId);

            return View(checkout);
        }
        
        /* Authorized function that returns the WishList view and all the books in wishlist */
        [Authorize]
        public async Task<IActionResult> Wishlist()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            // Service layer gets all the books from the wishlist 
            var books = _wishlistService.GetBooks(userId);
            return View(books);
        }

        /* Authorized function that adds a chosen book (id in parameter) to the users wishlist: */
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            _wishlistService.AddToWishlist(userId, id);

            return RedirectToAction("Index","Book");
        }

        /* Authorized function that returns a list of CartViewModels to the users OrderHistory View: */
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
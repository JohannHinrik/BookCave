using System;
using BookCave.Models.ViewModels;

namespace BookCave.Services
{
    public class SignUpService : ISignUpService
    {
        public void ProcessSignUp(SignUpViewModel signUp)
        {
            if (string.IsNullOrEmpty(signUp.FirstName)) { throw new Exception("First name is missing"); }
            if (string.IsNullOrEmpty(signUp.LastName)) { throw new Exception("Last name is missing"); }
            if (string.IsNullOrEmpty(signUp.UserName)) { throw new Exception("Username is missing"); }
            if (string.IsNullOrEmpty(signUp.Address)) { throw new Exception("Address is missing"); }
            if (string.IsNullOrEmpty(signUp.City)) { throw new Exception("City is missing"); }
            if (string.IsNullOrEmpty(signUp.Country)) { throw new Exception("Country is missing"); }
            if (string.IsNullOrEmpty(signUp.Email)) { throw new Exception("Email is missing"); }
            if (string.IsNullOrEmpty(signUp.Password)) { throw new Exception("Password is missing"); }
        }
    }
}
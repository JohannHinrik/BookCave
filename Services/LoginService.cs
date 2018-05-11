using System;
using BookCave.Models.ViewModels;

namespace BookCave.Services
{
    public class LoginService : ILoginService
    {
        public void ProcessForm(LoginViewModel login)
        {
            if (string.IsNullOrEmpty(login.Email)) { throw new Exception("Email is missing"); }
            if (string.IsNullOrEmpty(login.Password)) { throw new Exception("Password is missing"); }
        }
    }
}
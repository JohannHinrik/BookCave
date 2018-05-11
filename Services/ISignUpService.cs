using  BookCave.Models.ViewModels;

namespace BookCave.Services
{
    public interface ISignUpService
    {
        void ProcessSignUp(SignUpViewModel signUp);
    }
}
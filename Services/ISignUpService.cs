using  BookCave.Models.ViewModels;

namespace BookCave.Services
{
    public interface ISignUpService
    {
        void ProcessForm(SignUpViewModel signUp);
    }
}
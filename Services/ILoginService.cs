using BookCave.Models.ViewModels;
namespace BookCave.Services
{
    public interface ILoginService
    {
        void ProcessLogin(LoginViewModel login);
    }
}
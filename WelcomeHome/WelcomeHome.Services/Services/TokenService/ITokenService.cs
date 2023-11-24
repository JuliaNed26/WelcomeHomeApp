using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.Services
{
    public interface ITokenService
    {
        Task<string> GenerateAsync(User user);
    }
}

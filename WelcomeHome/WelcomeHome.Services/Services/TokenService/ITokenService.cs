using WelcomeHome.DAL.Models;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface ITokenService
    {
        Task<string> GenerateJwtAsync(User user);

        Task<string> GenerateNewRefreshTokenAsync(User user);
    }
}

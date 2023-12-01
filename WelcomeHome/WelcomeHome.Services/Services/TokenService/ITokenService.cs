using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.Services
{
    public interface ITokenService
    {
        Task<string> GenerateJwtAsync(User user);

        Task<string> GenerateNewRefreshTokenAsync(User user);
        Task UnvalidateTokensAsync(Guid userId);
    }
}

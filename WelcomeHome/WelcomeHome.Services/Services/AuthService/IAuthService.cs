using WelcomeHome.DAL.Models;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IAuthService
    {
        Task<TokensDTO> LoginUserAsync(UserLoginDTO user);
        Task<User?> RegisterUserAsync(UserRegisterDTO user, string? role = null);
        Task<TokensDTO> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync(Guid userId);
    }
}
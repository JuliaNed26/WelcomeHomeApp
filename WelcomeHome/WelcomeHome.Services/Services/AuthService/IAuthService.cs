using WelcomeHome.DAL.Models;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IAuthService
    {
        Task<string?> LoginUserAsync(UserLoginDTO user);
        Task<User?> RegisterUserAsync(UserRegisterDTO user, string? role = null);
    }
}

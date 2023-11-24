using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

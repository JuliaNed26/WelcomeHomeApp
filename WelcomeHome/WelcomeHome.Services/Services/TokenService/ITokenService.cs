using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.Services.TokenService
{
    public interface ITokenService
    {
        Task<string> GenerateAsync(User user);
    }
}

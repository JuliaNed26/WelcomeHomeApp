using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services.TokenService;

namespace WelcomeHome.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AuthService(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<string?> LoginUserAsync(UserLoginDTO user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser != null)
            {
                var result = await _userManager.CheckPasswordAsync(existingUser, user.Password);
                if (result) {
                    return await _tokenService.GenerateAsync(existingUser);
                }
            }

            return null;
         }

        public async Task<User?> RegisterUserAsync(UserRegisterDTO user, string? role = null)
        {
            var newUser = _mapper.Map<User>(user);
            newUser.UserName = user.Email;
            var result = await _userManager.CreateAsync(newUser, user.Password);

            if(result.Succeeded)
            {
                if(role != null)
                {
                    var identityRole = new IdentityRole<Guid>(role);
                    await _userManager.AddToRoleAsync(newUser, identityRole.Name);
                }

                return newUser;
            }

            return null;
        }
    }
}

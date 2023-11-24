using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public TokenService(IConfiguration configuration, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> GenerateAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                claims.Add(new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user))[0]));
            }

            var secret = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value);

            var key = new SymmetricSecurityKey(secret);

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}

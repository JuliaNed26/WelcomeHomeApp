using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;

namespace WelcomeHome.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        private readonly UserManager<User> _userManager;

        private readonly IUnitOfWork _unitOfWork;

        public TokenService(
            IConfiguration configuration,
            UserManager<User> userManager,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GenerateJwtAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                claims.Add(new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user))[0]));
            }

            if (user.Volunteer != null)
            {
                claims.Add(new Claim(nameof(Volunteer.IsVerified), user.Volunteer.IsVerified.ToString()));
            }

            var secret = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value);

            var key = new SymmetricSecurityKey(secret);

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<string> GenerateNewRefreshTokenAsync(User user)
        {
            var refreshTokenForUser = CreateRefreshTokenToSave();
            await _unitOfWork.RefreshTokenRepository.DeleteForUserAsync(user.Id).ConfigureAwait(false);
            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshTokenForUser).ConfigureAwait(false);
            return refreshTokenForUser.Token;

            RefreshToken CreateRefreshTokenToSave()
            {
                var refreshToken = new RefreshToken
                {
                    //Id = Guid.NewGuid(),
                    Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    Expires = DateTime.Now.AddDays(7),
                    UserId = user.Id
                };
                return refreshToken;
            }
        }

        public async Task UnvalidateTokensAsync(long userId)
        {
            _unitOfWork.RefreshTokenRepository.DeleteAllForUserAsync(userId).ConfigureAwait(false);
        }
    }
}
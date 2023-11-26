using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Exceptions;

namespace WelcomeHome.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public AuthService(
            UserManager<User> userManager,
            ITokenService tokenService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<TokensDto> LoginUserAsync(UserLoginDTO user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email)
                               ?? throw new BusinessException("Can not login non exist user");

            var result = await _userManager.CheckPasswordAsync(existingUser, user.Password);
            if (result)
            {
                var jwtToken = await _tokenService.GenerateJwtAsync(existingUser).ConfigureAwait(false);
                var refreshToken = await _tokenService.GenerateNewRefreshTokenAsync(existingUser).ConfigureAwait(false);
                return new()
                {
                    JwtToken = jwtToken,
                    RefreshToken = refreshToken,
                };
            }

            throw new BusinessException("User password was incorrect");

		}

        public async Task<User?> RegisterUserAsync(UserRegisterDTO user, string? role = null)
        {
            var newUser = _mapper.Map<User>(user);
            newUser.UserName = user.Email;
            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded)
            {
                if (role != null)
                {
                    var identityRole = new IdentityRole<Guid>(role);
                    await _userManager.AddToRoleAsync(newUser, identityRole.Name);
                }

                return newUser;
            }

            return null;
		}

		public async Task<TokensDto> RefreshTokenAsync(string refreshToken)
		{
			var foundRefreshToken = await _unitOfWork.RefreshTokenRepository
													 .GetByTokenAsync(refreshToken)
													 .ConfigureAwait(false);

			if (foundRefreshToken == null || foundRefreshToken.Expires < DateTime.UtcNow)
			{
				throw new BusinessException("Refresh token expired or do not exist. User should relogin");
			}

			var newJwtToken = await _tokenService
                                   .GenerateJwtAsync(foundRefreshToken.User)
                                   .ConfigureAwait(false);
			var newRefreshToken = await _tokenService
                                        .GenerateNewRefreshTokenAsync(foundRefreshToken.User)
                                        .ConfigureAwait(false);

			return new()
			{
				JwtToken = newJwtToken,
				RefreshToken = newRefreshToken,
			};
		}
	}
}
